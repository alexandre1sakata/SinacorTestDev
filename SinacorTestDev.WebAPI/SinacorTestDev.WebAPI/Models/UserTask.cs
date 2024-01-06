using Microsoft.OpenApi.Extensions;
using System.ComponentModel.DataAnnotations;
using System.Net.NetworkInformation;

namespace SinacorTestDev.WebAPI.Models;

public class UserTask
{
    public int Id { get; private set; }

    [Required(ErrorMessage = "Propriedade Name é obrigatória")]
    public string Name { get; private set; }

    [Required(ErrorMessage = "Propriedade Description é obrigatória")]
    public string Description { get; private set; }

    [Required(ErrorMessage = "Propriedade Status é obrigatória")]
    public string Status { get; private set; }

    public DateTime CreatedDate { get; private set; }
    public DateTime LastModifiedDate { get; private set; }

    public UserTask(int id, string name, string description, string status)
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
        => CreatedDate = DateTime.Now;

    public void SetLastModifiedDate()
        => LastModifiedDate = DateTime.Now;

    public void SetName(string newName)
        => Name = newName;

    public void SetDescription(string newdescription)
        => Description = newdescription;

    public string GetName()
        => Name;
}
