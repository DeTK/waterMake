using CirnoLib.Settings;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace waterMake.Settings
{
    public static class Settings
    {
        private static readonly CryptoRegistryComponent Reg = new CryptoRegistryComponent(@"DeTK\waterMake", "DeTK", true);
    }
}