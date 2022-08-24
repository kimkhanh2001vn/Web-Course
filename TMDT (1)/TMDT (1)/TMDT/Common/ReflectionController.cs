using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using System.Text.RegularExpressions;

namespace Common
{
    public class ReflectionController
    {
        //Lấy danh sách các Controller
        public List<Type> GetControllers(string namespaces)
        {
            IEnumerable<Type> types = Assembly.GetExecutingAssembly().GetTypes()
                .Where(type => typeof(Controller).IsAssignableFrom(type) && type.Namespace.Contains(namespaces))
                .OrderBy(x => x.Name);
            return types.ToList();
        }

        //Lấy danh sách các action theo controller
        public List<string> GetActions(Type controller)
        {
            IEnumerable<MemberInfo> memberInfo = controller
                .GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public)
                .Where(m => !m.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), true)
                .Any()).OrderBy(x => x.Name);

            List<string> listAction = new List<string>();
            foreach (MemberInfo method in memberInfo)
            {
                if (method.ReflectedType.IsPublic && !method.IsDefined(typeof(NonActionAttribute)))
                {
                    listAction.Add(method.Name.ToString());
                    //string name = method.Name;

                    //Add the Http method to name (distinguish between Edit( httpget) and Edit (httpost)
                    //Edit with no () is httpget
                    //Not test for AcceptVerbAttribute
                    //var CustomAttribute = method.GetCustomAttributes(typeof(ActionMethodSelectorAttribute));
                    //if(CustomAttribute.Any()) 
                    //{
                    //    foreach(var Attribute in CustomAttribute)
                    //    {
                    //        string AttributeName = Attribute.GetType().Name;
                    //        if(Regex.IsMatch(AttributeName,@"Http[a-zA-Z]+Attribute"))
                    //        {
                    //            name = name + "(" + AttributeName.Replace("Attribute", "") + ")"; //Edit(HttpPost)
                    //        }
                    //    }
                    //}    

                    //listAction.Add(name);
                }                
            }
            return listAction;
        }
    }
}