﻿
using System.Reflection.Metadata.Ecma335;

namespace Restaurant.Data.Models.IngredientModels
{
    public class IngredientAdminPanelWrapper
    {
        public List<IngredientAdminPanelItem> Items { get; set; }
        public int ItemsCount { get; set; }
    }
}