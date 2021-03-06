﻿using System;
using System.Linq;
using StructureMap;
using ProfileEngine.Infrastructure;
using ProfileEngine.Core.Interfaces;
using StructureMap.Configuration.DSL;

namespace ProfileEngine
{
    public class IoC
    {
        public static void Init()
        {
            ObjectFactory.Configure(config =>
                {
                    config.Scan(scan =>
                        {
                            scan.TheCallingAssembly();
                            scan.AssemblyContainingType<Message>();
                            scan.AssemblyContainingType<IRaiseEvent>();
                            scan.WithDefaultConventions();
                        });

                    config.AddRegistry<EventHandlerRegistry>();
                });
        }

        public class EventHandlerRegistry : Registry
        {
            public EventHandlerRegistry()
            {
                Scan(scan =>
                    scan.AddAllTypesOf<IHandleEvent<IDomainEvent>>()
                    );
            }
        }
    }
}
