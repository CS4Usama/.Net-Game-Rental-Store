﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameRentalStore.Models.ViewModels
{
    public class GameVM
    {
        public Game Game { get; set; }

        [ValidateNever]
        public IEnumerable<Game> Games { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> GenreList { get; set; }

        [ValidateNever]
        public Dictionary<int, List<GameRating>> GameRating { get; set; }
    }
}
