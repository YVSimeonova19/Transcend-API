﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Transcend.DAL.Models;

public class User : IdentityUser
{
    [ForeignKey(nameof(Carrier))]
    public int? CarrierId { get; set; }
    public virtual Carrier Carrier { get; set; } = new Carrier();

    [ForeignKey(nameof(UserDetails))]
    public int? UserDetailsId { get; set; }
    public virtual UserDetails UserDetails { get; set; } = new UserDetails();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
