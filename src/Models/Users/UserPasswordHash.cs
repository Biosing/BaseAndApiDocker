﻿using Models.Utils.Cryptography;
using Services.Utils;
using System;

namespace Models.ValueObjects
{
    public record UserPasswordHash
    {
        public static UserPasswordHash Random()
        {
            var pass =
                $"{new RandomCapital(1)}{new RandomLetter(6)}{new RandomDigital(3)}{new RandomSpecificSymbol(1)}";
            return new UserPasswordHash(pass);
        }

        private readonly string _password;
        private readonly Lazy<HashString> _asHash;

        public UserPasswordHash(string password)
        {
            password.ThrowIfNull(nameof(password));
            _password = password;
            _asHash = new Lazy<HashString>(() => new HashString(_password));
        }

        public string Value()
        {
            return (string)EnoughComplexityOrFail()._asHash.Value;
        }

        public UserPasswordHash EnoughComplexityOrFail()
        {
            new ComplexityScore(_password).ValidOrFail();

            return this;
        }

        public UserPasswordHash DoesNotContainEmailOrFail(string email)
        {
            return this;
        }

        public static explicit operator string(UserPasswordHash pass)
        {
            return pass?.Value();
        }
    }
}