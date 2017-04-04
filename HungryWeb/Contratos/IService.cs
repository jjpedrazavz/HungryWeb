using HungryWeb.Models3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Contrato encargado de implementar un CRUD generico para Lidear con Alimentos y Ordenes
/// </summary>
namespace HungryWeb.Contratos
{
    interface IService<T>
    {
        void CreateItem(T alimento);

        void UpdateItem(T alimento);

        void DeleteItem(T alimento);

        IEnumerable<T> GetAllItems();
    }
}
