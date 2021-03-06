﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace TSS.SharpedJs
{
    /// <summary>
    /// Attribute makes it clear for ValuesRedactor that this type has a method "public static T Parse (string str)", where T - the type of current object.
    /// With this attribute, you can create your own attributes, working in ValuesRedactor.
    /// <para></para>
    /// Атрибут дает понять ValuesRedactor что этот тип имеет метод "public static T Parse(string str)", где T - данный тип.
    /// С этим атрибутом вы можете создавать свои атрибуты, работающие в ValuesRedactor.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface)]
    class ParsebleAttribute : Attribute
    {
        public static object Parse(string str, Type typeToConvert)
        {
            MethodInfo parseMethod = typeToConvert.GetMethod("Parse", BindingFlags.Static | BindingFlags.Public);
            if (parseMethod == null || !typeToConvert.IsAssignableFrom(parseMethod.ReturnType))
                throw new ParsebleException("Parseble must have a public static method \"Parse\" " +
                "which get string and create from it new object with this type(Parseble type)!");
            return parseMethod.Invoke(null, str );
        }
    }

    class ParsebleException : Exception
    {
        public string MessageForUser { get; private set; }

        public ParsebleException(string msg, string messageForUser) : base(msg)
        {
            MessageForUser = messageForUser;
        }
        public ParsebleException() : base()
        {

        }
        public ParsebleException(string msg) : base(msg)
        {
            
        }
    }
}
