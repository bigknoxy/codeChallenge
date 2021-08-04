using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Verikai.Sample.Service.Services.Abstractions
{
    public interface IEncryptionService
    {
        public byte[] Encrypt(byte[] toEncrypt);
    }
}
