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
        Task<bool> CreateItem(OrderViewModel orden);

        Task<bool> UpdateItem(DetailedOrderViewModel orden);

        Task<bool> DeleteItem(int id);

        Task<IEnumerable<SlimOrdersViewModel>> GetAllSlimOrders();

        Task<DetailedOrderViewModel> GetDetailedOrder(int id);

        Task<OrderViewModel> GetCreateOrderForm();

    }
}
