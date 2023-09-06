﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicPortal.DAL.Entities;

namespace MusicPortal.DAL.Interfaces
{
    public interface ISaltRepository
    {
        Task<Salt> Get(User u);
        Task AddSItem(Salt s);
    }
}
