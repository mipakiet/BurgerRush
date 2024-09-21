﻿using System;
using UnityEngine;

namespace EditorHelpers{
    
    public class SingleEnumFlagSelectAttribute : PropertyAttribute
    {
        private Type enumType;

        public Type EnumType
        {
            get => enumType;
            set
            {
                if (value == null)
                {
                    Debug.LogError($"{GetType().Name}: EnumType cannot be null");
                    return;
                }
                if (!value.IsEnum)
                {
                    Debug.LogError($"{GetType().Name}: EnumType is {value.Name} this is not an enum");
                    return;
                }
                enumType = value;
                IsValid = true;
            }
        }
        
        public bool IsValid { get; private set; }
    }
}
