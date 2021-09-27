using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TodoAppFront.Models
{
    public class TodoModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O nome da tarefa é obrigatório", AllowEmptyStrings = false)]
        public string Title { get; set; }

        [Required(ErrorMessage = "O nome do usuário é obrigatório", AllowEmptyStrings = false)]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage =
            "Números e caracteres especiais não são permitidos no nome.")]
        public string Creator { get; set; }
        public string Description { get; set; }

        [Required(ErrorMessage = "O status é obrigatório", AllowEmptyStrings = false)]
        public TaskProgress Status { get; set; }

        public DateTime Date { get; set; }

        public enum TaskProgress
        {
            Backlog = 1,
            InProgress = 2,
            Completed = 3
        }
    }
}
