﻿// MBR / MBV
#define MBR

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemotableObjects
{
#if (MBV)
    [Serializable]
    public class RemoteClass
#else
    public class RemoteClass : MarshalByRefObject
#endif
    {        
        public RemoteClass()
        {
        }

        public string Init(string info)
        {
            return Cache.Instance.SendNotify(EAction.Init, info);
        }

        public string GetCommand()
        {
            return Cache.Instance.SendNotify(EAction.Connected, "");
        }

        public string Terminate()
        {
            return Cache.Instance.SendNotify(EAction.Release, "");
        }
    }
}
