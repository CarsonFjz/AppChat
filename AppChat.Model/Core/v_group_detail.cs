﻿using System;
using System.Linq;
using System.Text;

namespace  AppChat.Model
{
    public class v_group_detail
    {
        
        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int gid {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public int userid {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Byte urole {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public Byte upower {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string remarkname {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string nickname {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string sign {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string headphoto {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? addtime {get;set;}

    }
}