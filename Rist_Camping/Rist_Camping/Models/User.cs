﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rist_Camping.Models
{
    public enum Gender
    {
        male, female, notSpecified
    }

    public enum UserRole
    {
        registeredUser, admin, guest
    }

    public class User
    {
        private int _id;

        public int ID
        {
            get { return this._id; }
            set
            {
                if (value >= this._id)
                {
                    this._id = value;
                }
            }
        }

        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public Gender Gender { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
        public UserRole UserRole { get; set; }

        public User() : this(0, "", "", "", Gender.notSpecified, "", "", "", UserRole.guest) { }

        public User(int id, string username, string firstname, string lastname, Gender gender, string email, string password, string passwordConfirm, UserRole userRole)
        {
            this.ID = id;
            this.Username = username;
            this.Firstname = firstname;
            this.Lastname = lastname;
            this.Gender = gender;
            this.Email = email;
            this.Password = password;
            this.PasswordConfirm = passwordConfirm;
            this.UserRole = userRole;
        }
    }
}