using System.ComponentModel;

namespace SistemaDeTarefas.Enums
{
    public enum StatusTask
    {
        [Description("Pending work")]
        Pending = 0,
        [Description("Work in Progress")]
        Working = 1,
        [Description("Work done")]
        Done =2,

    }
}
