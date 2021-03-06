﻿using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Prng;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;
using System;

namespace PRI_NaszSamochód.MobileAuthentication
{
    public interface IKeysHolder
    {
        void GenerateKeys(int keySize);
        string SerializePublicKey();
        AsymmetricKeyParameter DeserializePublicKey(string serializedKey);
    }

    public class KeysHolder : IKeysHolder
    {

        private static readonly KeysHolder _instance = new KeysHolder();
        private AsymmetricKeyParameter privateKeyHolder;

        public AsymmetricKeyParameter PrivateKey
        {
            get { return privateKeyHolder; }
            set { privateKeyHolder = value; }
        }

        private AsymmetricKeyParameter publicKeyHolder;

        public AsymmetricKeyParameter PublicKey
        {
            get { return publicKeyHolder; }
            set { publicKeyHolder = value; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        private KeysHolder()
        {
            GenerateKeys(1024);
        }

        public static KeysHolder Instance
        {
            get
            {
                return _instance;
            }
        }

        public DateTime ExpirationDate { get; set; }

        /// <summary>
        /// Generating private and public keys for RSA algorithm with size = <code>keySize</code>
        /// </summary>
        /// <param name="keySize"></param>
        public void GenerateKeys(int keySize)
        {
            CryptoApiRandomGenerator randomGenerator = new CryptoApiRandomGenerator();
            SecureRandom secureRandom = new SecureRandom(randomGenerator);
            var keyGenerationParameters = new KeyGenerationParameters(secureRandom, keySize);

            var keyPairGenerator = new RsaKeyPairGenerator();
            keyPairGenerator.Init(keyGenerationParameters);
            var keyPair = keyPairGenerator.GenerateKeyPair();
            PrivateKey = keyPair.Private;
            PublicKey = keyPair.Public;
        }

        public string SerializePublicKey()
        {
            SubjectPublicKeyInfo publicKeyInfo = SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(PublicKey);
            byte[] serializedPublicBytes = publicKeyInfo.ToAsn1Object().GetDerEncoded();
            string serializedPublic = Convert.ToBase64String(serializedPublicBytes);
            return serializedPublic;
        }

        public string SerializePrivateKey()
        {
            PrivateKeyInfo privateKeyInfo = PrivateKeyInfoFactory.CreatePrivateKeyInfo(PrivateKey);
            byte[] serializedPrivateBytes = privateKeyInfo.ToAsn1Object().GetDerEncoded();
            string serializedPrivate = Convert.ToBase64String(serializedPrivateBytes);
            return serializedPrivate;
        }

        public AsymmetricKeyParameter DeserializePublicKey(string serializedKey)
        {
            return PublicKeyFactory.CreateKey(Convert.FromBase64String(serializedKey));
        }

        public AsymmetricKeyParameter DeserializePrivateKey(string serializedKey)
        {
            return PrivateKeyFactory.CreateKey(Convert.FromBase64String(serializedKey));
        }
    }
}