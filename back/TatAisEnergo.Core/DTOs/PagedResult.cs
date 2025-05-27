﻿namespace TatAisEnergo.Core.DTOs
{
    public class PagedResult<T>
    {
        public List<T> Items { get; set; } = new();

        public int Total { get; set; }
    }
}
