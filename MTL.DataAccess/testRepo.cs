﻿using System;
using System.Collections.Generic;
using System.Text;
using MTL.DataAccess.Contracts;
using MTL.DataAccess.Entities;

namespace MTL.DataAccess
{
    public class testRepo
    {
        private IRepositoryWrapper _repository;

        public testRepo(IRepositoryWrapper repository)
        {
            _repository = repository;
        }

        public void test()
        {
            var x = _repository.TimeLine.FindAll();

            AppUser appUser = new AppUser();
            
        }
    }
}
