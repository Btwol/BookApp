﻿using AutoMapper;
using BookApp.Server.Models;
using BookApp.Server.Services.Interfaces.MapperServices;
using BookApp.Shared.Data;

namespace BookApp.Server.Services.MapperServices
{
    public class HighlightMapperService : IHighlightMapperService
    {
        private readonly IMapper _mapper;

        public HighlightMapperService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public void MapEditHighlight(Highlight highlightToUpdate, HighlightModel updatedHighlightModel)
        {
            highlightToUpdate.LastNodeCharIndex = updatedHighlightModel.LastNodeCharIndex;
            highlightToUpdate.FirstNodeCharIndex = updatedHighlightModel.FirstNodeCharIndex;
            highlightToUpdate.PageNumber = updatedHighlightModel.PageNumber;
            highlightToUpdate.ElementId = int.Parse(updatedHighlightModel.ElementId);
            highlightToUpdate.FirstNodeIndex = updatedHighlightModel.FirstNodeIndex;
            highlightToUpdate.LastNodeIndex = updatedHighlightModel.LastNodeIndex;
            highlightToUpdate.NodeCount = updatedHighlightModel.NodeCount;
        }

        public Highlight MapToHighlight(HighlightModel highlightModel)
        {
            return _mapper.Map<Highlight>(highlightModel);
        }

        public HighlightModel MapToHighlightModel(Highlight highlight)
        {
            return _mapper.Map<HighlightModel>(highlight);
        }
    }
}
