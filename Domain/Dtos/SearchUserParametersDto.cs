using Domain.Models;

namespace Domain.Dtos;

public class SearchUserParametersDto
{
    public string? Department { get; }
    public string? SkillOne { get;  }
    public Proficiency? ReqScoreOne { get; }
    public string? SkillTwo { get;  }
    public Proficiency? ReqScoreTwo { get; }
    public string? SkillThree { get;  }
    public Proficiency? ReqScoreThree { get; }
   

//search user params constructers df
    public SearchUserParametersDto(string? department, string?  skillOne, Proficiency? reqScoreOne, string? skillTwo, Proficiency? reqScoreTwo, string? skillThree, Proficiency? reqScoreThree)
    {
        Department = department;
        SkillOne = skillOne;
        ReqScoreOne = reqScoreOne;
        SkillTwo = skillTwo;
        ReqScoreTwo = reqScoreTwo;
        SkillThree = skillThree;
        ReqScoreThree = reqScoreThree;
    }
}