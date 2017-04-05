using HungryWeb.Models3;
using HungryWeb.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Contrato encargado de implementar un CRUD generico para Lidear con Alimentos
/// </summary>
namespace HungryWeb.Contratos
{
    interface IServiceOrders
    {
        void CreateItem(OrdenesViewModel orden);

        void UpdateItem(int id, OrdenesViewModel orden);

        void DeleteItem(int id);

        Task<IEnumerable<SlimOrdersViewModel>> GetAllSlimOrders();

        Task<DetailedOrderViewModel> GetDetailedOrder(int id);

    }
}
