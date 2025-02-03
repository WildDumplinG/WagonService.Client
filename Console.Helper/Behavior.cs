
using Newtonsoft.Json;

namespace Console.Helper
{
    public static class Behavior
    {
        public static void Execute(this object value, string name, params string[] args)
        {
            try
            {
                var methods = value.GetType().GetMethods().Where(m => m.Name == name && m.GetParameters().Length == args.Length);

                foreach (var method in methods)
                {
                    var parametrs = method.GetParameters();

                    var listParam = new List<object>();

                    if (parametrs.Length == args.Length)
                    {
                        var count = 0;

                        foreach (var param in parametrs)
                        {
                            if (param.ParameterType.IsArray)
                            {
                                var subs = args[count].Split(";");

                                var nameTypeArray = param.ParameterType.FullName;

                                var typeArray = Type.GetType(nameTypeArray!.Remove(nameTypeArray.Length - 2));

                                var methodsParam = typeArray!.GetMethods().Where(m =>
                                {
                                    return m.Name == "TryParse" && m.GetParameters().Length == 2 && m.GetParameters()[0].ParameterType.Name == "String";
                                }).ToArray();

                                var genericType = typeof(List<>).MakeGenericType(typeArray);
                                // your code: var genericType = typeof(List<>).MakeGenericType(type);
                                var list = Activator.CreateInstance(genericType);

                                if (typeArray.Name == "String")
                                {
                                    foreach (var sub in subs)
                                    {
                                        object[] o = new object[] { sub };

                                        list!.GetType().GetMethod("Add")!.Invoke(list, o);

                                    }
                                }
                                else
                                {
                                    foreach (var sub in subs)
                                    {
                                        var objmethodsParam = Activator.CreateInstance(typeArray);

                                        object[] arg = new object[2] { sub, objmethodsParam! };

                                        methodsParam[0].Invoke(value, arg);

                                        object[] o = new object[] { arg[1] };

                                        list!.GetType().GetMethod("Add")!.Invoke(list, o);

                                    }
                                }

                                listParam.Add(list!.GetType().GetMethod("ToArray")?.Invoke(list, new object[] { })!);
                            }
                            else
                            {
                                if (param.ParameterType.Name != "String" && param.ParameterType.BaseType!.Name != "Enum")
                                {
                                    var methodsParam = param.ParameterType.GetMethods().Where(m =>
                                    {
                                        return m.Name == "TryParse" && m.GetParameters().Length == 2 && m.GetParameters()[0].ParameterType.Name == "String";
                                    }).ToArray();


                                    if (methodsParam.Length == 1)
                                    {
                                        var objmethodsParam = Activator.CreateInstance(param.ParameterType);

                                        object[] arg = new object[2] { args[count], objmethodsParam! };

                                        methodsParam[0].Invoke(value, arg);

                                        listParam.Add(arg[1]);
                                    }
                                }
                                else if (param.ParameterType.Name == "String")
                                {
                                    listParam.Add(args[count]);

                                }
                                else if (param.ParameterType.BaseType!.Name == "Enum")
                                {
                                    var methodsParam = param.ParameterType.BaseType.GetMethods().Where(m =>
                                    {
                                        return m.Name == "TryParse" && m.GetParameters().Length == 2 && m.GetParameters()[0].ParameterType.Name == "String";
                                    }).ToArray();

                                    if (methodsParam.Length == 1)
                                    {
                                        var objmethodsParam = Activator.CreateInstance(param.ParameterType, true);

                                        var enumVals = new List<string>();

                                        var enVal = new List<object>();

                                        foreach (var val in Enum.GetValues(param.ParameterType))
                                        {
                                            enumVals.Add(val.ToString()!);
                                            enVal.Add(val);
                                        }

                                        var index = enumVals.IndexOf(args[count]);

                                        listParam.Add(enVal[index]);
                                    }
                                }
                                else if (param.ParameterType.BaseType.Name == "object")
                                {
                                    listParam.Add(JsonConvert.SerializeObject(args[count], Formatting.Indented));
                                }

                            }
                            count++;
                        }
                    }

                    var paramsLenght = method.GetParameters().Length;

                    if (paramsLenght == listParam.Count)
                    {
                        method.Invoke(value, listParam.ToArray());
                    }
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }

        }
    }
}
