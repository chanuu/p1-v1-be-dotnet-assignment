﻿using Domain.Aggregates.FlightAggregate;
using Domain.Common;
using Domain.Events;
using Domain.Events.Orders;
using Domain.Exceptions;
using Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregates.OrderAggregate
{
   public class Order: Entity, IAggregateRoot
    {
        public DateTimeOffset OrderedDate { get; private set; }

        public string OrderNo { get; set; }

        public Guid CustomerId { get; private set; }

        public Guid FlightId { get; private set; }

        public string Status { get; set; }

        private List<OrderItem> _items;
        public IReadOnlyCollection<OrderItem> Items => _items;


        public Order() { 
           _items= new List<OrderItem>();
        }
        public Order( Guid customerId, Guid flightId, string status,List<OrderItem> items):this()
        {
            OrderedDate = DateTime.Now;
            CustomerId = customerId;
            FlightId = flightId;
            Status = status;
           _items = items;
          
        }

        public void AddItem(Guid flightRateId, double price, int qty)
        {
            var item = new OrderItem(flightRateId, price, qty);
            _items.Add(item);
        }

        public void IsConfimOrder()
        {
            //todo : implment order status enum
           if(this.Status== "Confirm")
            {
                throw new OrderDomainException("You Cannot Change status of  Confrim order !");
            }
            else
            {
                this.Status = "Confirm";
                AddDomainEvent(new OrderConfirmEvent(this));
            }
        }

        public void SetId(Guid id)
        {
            this.Id = id;
        }

        public void SetCutomerId(Guid cutomerId)
        {
            this.CustomerId = cutomerId;
        }

        public void SetFlightId(Guid flightId)
        {
            this.FlightId = flightId;
        }

        public void SetStatus(string status)
        {
            this.Status = status;
        }

        public void SetItems(List<OrderItem> items)
        {
            this._items= items;
        }
    }
}
