﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Foundation.Events
{
    public interface ISubscribe
    {
        void Handle(object domainEvent);
    }
}
