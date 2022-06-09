﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Howest.MagicCards.Shared.Extensions
{
    public static class EntityExtensions
    {
        public static IQueryable<T> ToPagedList<T>(this IQueryable<T> entities, int pageNumber, int pageSize)
        {
            return entities.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }
    }
}
