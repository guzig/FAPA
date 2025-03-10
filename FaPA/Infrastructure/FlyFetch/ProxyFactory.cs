﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace FaPA.Infrastructure.FlyFetch
{
    public class ProxyFactory
    {
        static readonly AssemblyBuilder AssemblyBuilder;
        static readonly ModuleBuilder ModuleBuilder;
        static readonly Dictionary<Type, Type> Cache = new Dictionary<Type, Type>();
        static ProxyFactory()
        {
            var name = new AssemblyName("pageable_proxies");
            AssemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(name, AssemblyBuilderAccess.RunAndSave);
            ModuleBuilder = AssemblyBuilder.DefineDynamicModule("main","pageable_proxies.dll");
           
        }

        public static T CreateProxy<T>(IPageHit hitChecker)
        {
            return (T) Activator.CreateInstance(CreateProxyType(typeof(T)), hitChecker);
        }

        static Type CreateProxyType<T>()
        {
            Type parent = typeof(T);
            return CreateProxyType(parent);
        }

        static Type CreateProxyType(Type parent)
        {
            lock (Cache)
            {
                if (Cache.ContainsKey(parent))
                {
                    return Cache[parent];
                }
           
                var tbuilder = ModuleBuilder.DefineType(parent.FullName + "__proxy", TypeAttributes.Class | TypeAttributes.Public,parent,new[]{typeof(IPageableElement)});
                //backing fields
                
                var pageIndexField = tbuilder.DefineField("pageIndex", typeof(int), FieldAttributes.Private);
                var loadedField = tbuilder.DefineField("loaded", typeof(bool), FieldAttributes.Private);
                var hitField = tbuilder.DefineField("hit", typeof(IPageHit), FieldAttributes.Private);
            
                //properties
                var rowIndex = tbuilder.DefineProperty("PageIndex", PropertyAttributes.HasDefault, typeof(int),Type.EmptyTypes);
                var loaded = tbuilder.DefineProperty("Loaded", PropertyAttributes.HasDefault, typeof(bool), Type.EmptyTypes);
                
                const MethodAttributes interfaceImpl = MethodAttributes.Public |
                                                       MethodAttributes.SpecialName | MethodAttributes.HideBySig|MethodAttributes.NewSlot|MethodAttributes.Virtual;

                var set_PageIndex = tbuilder.DefineMethod("set_PageIndex", interfaceImpl, null, new[] { typeof(int) });
                EmitSimpleSetter(pageIndexField, set_PageIndex);
                var get_PageIndex = tbuilder.DefineMethod("get_PageIndex", interfaceImpl, typeof(int), Type.EmptyTypes);
                EmitSimpleGetter(pageIndexField, get_PageIndex);
                rowIndex.SetGetMethod(get_PageIndex);
                rowIndex.SetSetMethod(set_PageIndex);

                var set_Loaded = tbuilder.DefineMethod("set_Loaded", interfaceImpl, null, new[] { typeof(bool) });
                EmitSimpleSetter(loadedField, set_Loaded);
                var get_Loaded = tbuilder.DefineMethod("get_Loaded", interfaceImpl, typeof(bool), Type.EmptyTypes);
                EmitSimpleGetter(loadedField, get_Loaded);
                loaded.SetGetMethod(get_Loaded);
                loaded.SetSetMethod(set_Loaded);

                //Ctor

                var ctor = tbuilder.DefineConstructor(MethodAttributes.HideBySig|MethodAttributes.SpecialName|MethodAttributes.RTSpecialName|MethodAttributes.Public,CallingConventions.Standard,new[]{typeof(IPageHit)});
                var gen = ctor.GetILGenerator();
                gen.Emit(OpCodes.Ldarg_0);
                gen.Emit(OpCodes.Call, tbuilder.BaseType.GetConstructor(Type.EmptyTypes));
                gen.Emit(OpCodes.Ldarg_0);
                gen.Emit(OpCodes.Ldarg_1);
                gen.Emit(OpCodes.Stfld,hitField);
                gen.Emit(OpCodes.Ret);


                foreach (var property in parent.GetProperties(BindingFlags.DeclaredOnly|BindingFlags.Instance|BindingFlags.Public).Where(p => p.CanRead && p.GetGetMethod().IsVirtual))
                {
                    OverrideGetter(tbuilder,property,get_Loaded,hitField,get_PageIndex);
                }

                var tt = tbuilder.CreateType();
                Cache[parent] = tt;
                
#if SAVEASM
                try
                {
                    assemblyBuilder.Save("pageable_proxies.dll");
                }
                catch (InvalidOperationException)
                {
                    //save twice not allowed
                }
#endif
            }
            return Cache[parent];
        }

        private static void OverrideGetter(TypeBuilder tbuilder
                                            , PropertyInfo property
                                            ,MethodBuilder getLoaded
                                            ,FieldBuilder hitField
                                            , MethodBuilder getPageIndex
                                            )
        {
            var methodName = property.GetGetMethod().Name;

            var pBuilder = tbuilder.DefineProperty(property.Name, property.Attributes,property.PropertyType,Type.EmptyTypes);

            var getter = tbuilder.DefineMethod(methodName
                                                    ,MethodAttributes.Public|MethodAttributes.Virtual|MethodAttributes.HideBySig|MethodAttributes.SpecialName
                                                    , property.PropertyType
                                                    , Type.EmptyTypes 
                                                    );

            var gen = getter.GetILGenerator();
            
            gen.Emit(OpCodes.Ldarg_0);
            gen.Emit(OpCodes.Call, getLoaded);
            gen.Emit(OpCodes.Brtrue_S,(byte)0x11);
            gen.Emit(OpCodes.Ldarg_0);
            gen.Emit(OpCodes.Ldfld, hitField);
            gen.Emit(OpCodes.Ldarg_0);
            gen.Emit(OpCodes.Call, getPageIndex);
            gen.Emit(OpCodes.Callvirt, typeof(IPageHit).GetMethod("Hit"));
            gen.Emit(OpCodes.Ldarg_0);
            gen.Emit(OpCodes.Call, property.GetGetMethod());
            gen.Emit(OpCodes.Ret);
            pBuilder.SetGetMethod(getter);
        }

        private static void EmitSimpleGetter(FieldBuilder field, MethodBuilder method)
        {
            var gen = method.GetILGenerator();
            gen.Emit(OpCodes.Ldarg_0);
            gen.Emit(OpCodes.Ldfld, field);
            gen.Emit(OpCodes.Ret);
        }

        private static void EmitSimpleSetter(FieldBuilder field, MethodBuilder method)
        {
            var gen = method.GetILGenerator();
            gen.Emit(OpCodes.Ldarg_0);
            gen.Emit(OpCodes.Ldarg_1);
            gen.Emit(OpCodes.Stfld, field);
            gen.Emit(OpCodes.Ret);
        }

    }
}
