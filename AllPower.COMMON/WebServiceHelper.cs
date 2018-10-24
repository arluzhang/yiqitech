using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net;
using System.IO;
using System.Web.Services.Description;
using System.CodeDom;
using Microsoft.CSharp;
using System.CodeDom.Compiler;


namespace AllPower.Common
{
    public class WebServiceHelper
    {
        public WebServiceHelper()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        #region InvokeWebService

        public static object ClientRecordWebService(string methodname, object[] args)
        {
            string url = "http://cmsuser.AllPower.net/sysadmin/ClientRecord/ClientRecordService.asmx";
            //string url = "http://localhost:10658/sysadmin/ClientRecord/ClientRecordService.asmx";
            string @namespace = "KingTopDev.Web.SysAdmin.ClientRecord";
            return WebServiceHelper.InvokeWebService(url, @namespace, null, methodname, args);
        }


        /// <summary>
        /// 根据指定的信息，调用远程WebService方法
        /// </summary>
        /// <param name="url">WebService的http形式的地址</param>
        /// <param name="namespace">欲调用的WebService的命名空间</param>
        /// <param name="classname">欲调用的WebService的类名（不包括命名空间前缀）</param>
        /// <param name="methodname">欲调用的WebService的方法名</param>
        /// <param name="args">参数列表</param>
        /// <returns>WebService的执行结果</returns>
        /// <remarks>
        /// 如果调用失败，将会抛出Exception。请调用的时候，适当截获异常。
        /// 异常信息可能会发生在两个地方：
        /// 1、动态构造WebService的时候，CompileAssembly失败。
        /// 2、WebService本身执行失败。
        /// </remarks>
        /// <example>
        /// <code>
        /// object obj = InvokeWebservice("http://localhost/GSP_WorkflowWebservice/common.asmx","Genersoft.Platform.Service.Workflow","Common","GetToolType",new object[]{"1"});
        /// </code>
        /// </example>
        public static object InvokeWebService(string url, string @namespace, string classname, string methodname, object[] args)
        {
            if ((classname == null) || (classname == ""))
            {
                classname = WebServiceHelper.GetWsClassName(url);
            }

            try
            {
                //获取WSDL
                WebClient wc = new WebClient();

                Stream stream = wc.OpenRead(url + "?WSDL");

                ServiceDescription sd = ServiceDescription.Read(stream);

                ServiceDescriptionImporter sdi = new ServiceDescriptionImporter();

                sdi.AddServiceDescription(sd, "", "");

                CodeNamespace cn = new CodeNamespace(@namespace);

                //生成客户端代理类代码

                CodeCompileUnit ccu = new CodeCompileUnit();

                ccu.Namespaces.Add(cn);

                sdi.Import(cn, ccu);

                CSharpCodeProvider csc = new CSharpCodeProvider();

                ICodeCompiler icc = csc.CreateCompiler();

                //设定编译参数

                CompilerParameters cplist = new CompilerParameters();

                cplist.GenerateExecutable = false;

                cplist.GenerateInMemory = true;

                cplist.ReferencedAssemblies.Add("System.dll");

                cplist.ReferencedAssemblies.Add("System.XML.dll");

                cplist.ReferencedAssemblies.Add("System.Web.Services.dll");

                cplist.ReferencedAssemblies.Add("System.Data.dll");
                //编译代理类

                CompilerResults cr = icc.CompileAssemblyFromDom(cplist, ccu);

                if (true == cr.Errors.HasErrors)
                {

                    System.Text.StringBuilder sb = new System.Text.StringBuilder();

                    foreach (System.CodeDom.Compiler.CompilerError ce in cr.Errors)
                    {

                        sb.Append(ce.ToString());

                        sb.Append(System.Environment.NewLine);

                    }

                    throw new Exception(sb.ToString());

                }

                //生成代理实例，并调用方法

                System.Reflection.Assembly assembly = cr.CompiledAssembly;

                Type t = assembly.GetType(@namespace + "." + classname, true, true);

                object obj = Activator.CreateInstance(t);

                System.Reflection.MethodInfo mi = t.GetMethod(methodname);

                return mi.Invoke(obj, args);

            }

            catch (Exception ex)
            {
                //throw new Exception(ex.InnerException.Message, new Exception(ex.InnerException.StackTrace));
                return -99;
            }
        }

        private static string GetWsClassName(string wsUrl)
        {

            string[] parts = wsUrl.Split('/');

            string[] pps = parts[parts.Length - 1].Split('.');

            return pps[0];

        }

        #endregion
    }
}
