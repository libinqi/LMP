﻿using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace LMP.QuestionSystem.Services.Dtos
{
    public class SubmitAnswerInput : IInputDto
    {
        [Range(1, int.MaxValue)]
        public int QuestionId { get; set; }

        [Required]
        public string Text { get; set; }
    }
}