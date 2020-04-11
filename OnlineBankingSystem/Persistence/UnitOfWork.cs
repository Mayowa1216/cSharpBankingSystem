﻿using OnlineBankingSystem.Core.Repositories;
using OnlineBankingSystem.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineBankingSystem.Persistence
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IEmailService cs { get; private set; }
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            cs = new EmailServiceRepository();
        }
    }
}