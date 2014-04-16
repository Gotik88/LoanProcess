// ============================================================================
// <copyright file="DomainEvents.cs" company="Dmytro Romanii">
//   Copyright (c) Dmytro Romanii 2014. All rights reserved.
// </copyright>
// ============================================================================

namespace LoanProcess.BusinessLogic.DomainEvents
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    /// <summary>
    /// have the ability for any class within our domain model to raise such an event,
    ///  which is easily accomplished with the following static class that makes use of a container like Unity, Castle, or Spring.NET
    /// </summary>
    public static class DomainEvents
    {
        [ThreadStatic] //so that each thread has its own callbacks
        private static List<Delegate> actions;

        // public IContainer Container { get; set; } //as before

        //Registers a callback for the given domain event
        public static void Register<T>(Action<T> callback) where T : IDomainEvent
        {
            if (actions == null)
                actions = new List<Delegate>();

            actions.Add(callback);
        }

        //Clears callbacks passed to Register on the current thread
        public static void ClearCallbacks()
        {
            actions = null;
        }

        //Raises the given domain event
        public static void Raise<T>(T args) where T : IDomainEvent
        {
            /*  foreach (var handler in Container.ResolveAll<Handles<T>>())
              {
                  handler.Handle(args);
              }

              if (actions != null)
              {
                  foreach (var action in actions)
                  {
                      if (action is Action<T>)
                      {
                          ((Action<T>)action)(args);
                      }
                  }
              }*/
        }
    }
}
