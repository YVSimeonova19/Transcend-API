﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transcend.BLL.Contracts;

public interface ICurrentUser
{
    string UserId { get; }
}
