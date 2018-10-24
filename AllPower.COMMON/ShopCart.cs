using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Web;

#region 版权注释
/*================================================================
    Copyright (C) 2010 华强北在线
    作者:     毛文勇
    创建时间： 2010年11月16日
    功能描述： 购物车类
    ===============================================================*/
#endregion

namespace AllPower.Common
{
    /// <summary>
    /// [购物车具体]商品类
    /// </summary>
    [Serializable]
    public class ShopCartItem
    {
        #region 私有成员
        private string _id;//产品ID
        private string _price = "0";//产品单价(结算价格)
        private int _quantity = 1;//产品数量
        private string _attribute;//产品属性(如:黑色，42码)
        public Hashtable _AttributeNum = new Hashtable();
        #endregion

        #region 属性
        /// <summary>
        /// 商品ID
        /// </summary>
        public string ID
        {
            get { return this._id; }
            set { this._id = value; }
        }

        /// <summary>
        /// 商品单价
        /// </summary>
        public string Price
        {
            get { return this._price; }
            set { this._price = value; }
        }

        /// <summary>
        /// 商品数量
        /// </summary>
        public int Quantity
        {
            get { return this._quantity; }
            set { this._quantity = value; }
        }

        /// <summary>
        /// 商品属性(如:黑色,42码)
        /// </summary>
        public string Attribute
        {
            get { return GB2312UnicodeConverter.ToGB2312(this._attribute); }
            set { this._attribute = GB2312UnicodeConverter.ToUnicode(value); }
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ID">产品ID</param>
        /// <param name="Price">产品单价(计算价)</param>
        /// <param name="Quantity">数量</param>
        /// /// <param name="Attribute">属性</param>
        public ShopCartItem(string ID, string Price, int Quantity, string Attribute)
        {
            _id = ID;
            _quantity = Quantity;
            _price = Price;
            _attribute = GB2312UnicodeConverter.ToUnicode(Attribute);

            _AttributeNum.Add(_attribute, Quantity);
        }
        #endregion
    }

    /// <summary>
    /// 购物车类
    /// </summary>
    [Serializable]
    public class ShopCart
    {
        

        public Hashtable _CartItems = new Hashtable();

        /// <summary>
        /// 构造函数
        /// </summary>
        public ShopCart()
        {

        }

        /// <summary>
        /// 返回购物车中的所有商品(接口类型)
        /// </summary>
        public ICollection CartItems
        {
            get { return _CartItems.Values; }
        }

        /// <summary>
        /// 购物中所有商品的价格合计
        /// </summary>
        public double TotalPrice
        {
            get
            {
                double sum = 0;
                foreach (ShopCartItem item in _CartItems.Values)
                {
                    foreach (int i in item._AttributeNum.Values)
                    {
                        sum += Convert.ToDouble(item.Price) * i;
                    }
                }
                return sum;
            }
        }

        /// <summary>
        /// 返回购物车里所有商品的数量
        /// </summary>
        public double TotalNum
        {
            get
            {
                double sum = 0;
                foreach (ShopCartItem item in _CartItems.Values)
                {
                    foreach (int i in item._AttributeNum.Values)
                    {
                        sum += i;
                    }
                }
                return sum;
            }
        }

        /// <summary>
        /// 向购物车里添加某商品
        /// </summary>
        /// <param name="ID">商品ID</param>
        /// <param name="Price">商品单价</param>
        /// <param name="Quantity">商品数量</param>
        /// <param name="Attribute">商品属性</param>
        public void AddItem(string ID, string Price, int Quantity, string Attribute)
        {
            if (Attribute == "")
            {
                Attribute = "attribute";
            }
            ShopCartItem item = (ShopCartItem)_CartItems[ID];
            if (item == null)
                _CartItems.Add(ID, new ShopCartItem(ID, Price, Quantity, Attribute));
            else
            {
                Attribute = GB2312UnicodeConverter.ToUnicode(Attribute);
                if (item._AttributeNum.ContainsKey(Attribute))
                {
                    item._AttributeNum[Attribute] = Utils.ParseInt(item._AttributeNum[Attribute].ToString(), 1) + Quantity;
                }
                else
                {
                    item._AttributeNum[Attribute] = Quantity;
                }
                item.Quantity = item.Quantity + Quantity;
                _CartItems[ID] = item;
            }
        }

        /// <summary>
        /// 从购物车里移除某商品
        /// </summary>
        /// <param name="ID">商品ID</param>
        public void RemoveItem(string ID,string Attribute)
        {
            ShopCartItem item = (ShopCartItem)_CartItems[ID];
            if (item == null)
            {
                return;
            }
            else
            {
                Attribute = Utils.UrlDecode(GB2312UnicodeConverter.ToUnicode(Attribute));
                item._AttributeNum.Remove(Attribute);
                _CartItems[ID] = item;
            }
        }

        /// <summary>
        /// 从购物车里移除某商品
        /// </summary>
        /// <param name="ID">商品ID</param>
        public void RemoveItem(string ID)
        {
            ShopCartItem item = (ShopCartItem)_CartItems[ID];
            if (item == null)
            {
                return;
            }
            else
            {
                _CartItems.Remove(item.ID);
            }
        }

        /// <summary>
        /// 修改购物车里某商品的数量
        /// </summary>
        /// <param name="ID">商品ID</param>
        /// <param name="Quantity">商品数量</param>
        public void UpdateItem(string ID, string Quantity,string Attribute)
        {
            string[] productID = ID.Split(',');
            string[] quantity = Quantity.Split(',');
            string[] attribute = Attribute.Split(',');
            for (int i = 0; i < productID.Length; i++)
            {
                if (productID[i] != "")
                {
                    ShopCartItem item = (ShopCartItem)_CartItems[productID[i]];
                    if (item == null)
                    {
                        continue;
                    }
                    else
                    {
                        int num = Utils.ParseInt(quantity[i], 1);
                        if (num > 0) //商品数量必须大于0
                        {
                            item._AttributeNum[attribute[i]] = num;
                        }
                        _CartItems[productID[i]] = item;
                    }
                }
            }
        }

        /// <summary>
        /// 清空购物车
        /// </summary>
        public void ClearCart()
        {
            _CartItems.Clear();
            Utils.WriteCookie(SystemConst.CARTCOOKIE, "",-1000);
        }

        /// <summary>
        /// 将ShopCart写入Cookie
        /// </summary>
        /// <param name="SC">ShopCart对象</param>
        /// <param name="CookieName">CookieName，默认为ShopCart</param>
        public void ShopCartToCookie(ShopCart SC)
        {
            if (SC == null)
            {
                return;
            }
            IFormatter fm = new BinaryFormatter();
            Stream sm = new MemoryStream();
            fm.Serialize(sm, SC);
            sm.Seek(0, SeekOrigin.Begin);
            StreamReader reader = new StreamReader(sm);
            string strCart = reader.ReadToEnd();
            reader.Close();
            Utils.WriteCookie(SystemConst.CARTCOOKIE, HttpContext.Current.Server.UrlEncode(strCart), 60 * 24);
        }

        /// <summary>
        /// 将Cookie反序列化为ShopCart
        /// </summary>
        public ShopCart CookieToShopCart()
        {
            string cart = Utils.GetCookie(SystemConst.CARTCOOKIE);
            if (cart == "")
            {
                return new ShopCart();
            }
            byte[] bt = System.Text.Encoding.Default.GetBytes(HttpContext.Current.Server.UrlDecode(cart));
            Stream sm = new MemoryStream(bt);
            IFormatter fm = new BinaryFormatter();
            ShopCart SC = (ShopCart)fm.Deserialize(sm);
            return SC;
        }
    }
}

