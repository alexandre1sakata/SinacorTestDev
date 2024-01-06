using Microsoft.OpenApi.Extensions;
using System.ComponentModel.DataAnnotations;
using System.Net.NetworkInformation;

namespace SinacorTestDev.WebAPI.Models;

public class UserTask
{
    private int Id { get; set; }

    [Required(ErrorMessage = "Propriedade Name é obrigatória")]
    private string Name { get; set; }

    [Required(ErrorMessage = "Propriedade Description é obrigatória")]
    private string? Description { get; set; }

    [Required(ErrorMessage = "Propriedade Status é obrigatória")]
    private string? Status { get; set; }

    private DateTime CreatedDate { get; set; }
    private DateTime LastModifiedDate { get; set; }

    public UserTask(int id, string name, string? description, string? status)
    {
        Id = id;
        Name = name;
        Description = description;
        Status = status;
    }

    public void ChangeStatus(string status)
    {
        switch (status.ToLower())
        {
            case "iniciada":
                Status = StatusTask.Iniciada.GetDisplayName();
                break;
            case "finalizada":
                Status = StatusTask.Finalizada.GetDisplayName();
                break;
            default:
                Status = StatusTask.Pendente.GetDisplayName();
                break;
        }
    }

    public void SetCreatedDate()
    {
        CreatedDate = DateTime.Now;
    }

    public void SetLastModifiedDate()
    {
        LastModifiedDate = DateTime.Now;
    }
}
