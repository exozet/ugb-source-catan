﻿using UnityEngine;
using UnityEditor;
using System.Collections;


namespace UGB.Core.Templates
{
    public class TemplateUGB : BaseTemplate
    {
        [MenuItem("Assets/Add/UGBClass" )]
        public static void AddssItem()
        {
            Create(new TemplateUGB());
        }
        
        public override string fileType
        {
            get
            {
                return ".cs";
            }            
        }
    
        public override string content
        {
            get
            {
                return 
                
@"using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UGB.Core;
using UGB.Core.Extensions;

public class " + name + @" : GameComponent
{
    public void Start ()
    {
        
    }
    
    public void Update()
    {
    
    }
}

";            
            }
        }
    }
}