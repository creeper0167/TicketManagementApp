﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TicketManagementApp.Models;

namespace TicketManagementApp.Context
{
    public class TkContext : DbContext
    {
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketGroup> TicketGroups { get; set; }
    }
}