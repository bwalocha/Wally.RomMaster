﻿using System;
using System.Threading;
using System.Threading.Tasks;

using MassTransit;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Wally.RomMaster.FileService.Application.Messages.Files;
using Wally.RomMaster.FileService.Infrastructure.DI.Microsoft.Models;
using Wally.RomMaster.FileService.Infrastructure.Messaging;
using Wally.RomMaster.FileService.Infrastructure.Messaging.Consumers;
using Wally.RomMaster.HashService.Application.Messages.Hashes;

namespace Wally.RomMaster.FileService.Infrastructure.DI.Microsoft.Extensions;

public static class MessagingExtensions
{
	public static IServiceCollection AddMessaging(this IServiceCollection services, AppSettings settings)
	{
		services.AddMassTransit(
			a =>
			{
				a.AddConsumers(typeof(IInfrastructureMessagingAssemblyMarker).Assembly);

				switch (settings.MessageBroker)
				{
					case MessageBrokerType.None:
						services.AddSingleton<IBus, BusStub>();
						break;
					case MessageBrokerType.AzureServiceBus:
						a.UsingAzureServiceBus(
							(host, cfg) =>
							{
								cfg.Host(settings.ConnectionStrings.ServiceBus);
								cfg.ConfigureEndpoints(host, new DefaultEndpointNameFormatter(".", "", true));
							});
						break;
					case MessageBrokerType.Kafka:
						a.UsingInMemory((context, config) => config.ConfigureEndpoints(context));

						// a.UsingRabbitMq((context, cfg) => cfg.ConfigureEndpoints(context));

						a.AddRider(rider =>
							{
								rider.AddConsumersFromNamespaceContaining<IInfrastructureMessagingAssemblyMarker>();
								// TODO: auto-register
								// rider.AddConsumer<KafkaMessageConsumer>();
								// rider.AddProducer<FileCreatedMessage>(nameof(FileCreatedMessage));
								// rider.AddProducer<FileModifiedMessage>(nameof(FileModifiedMessage));
								rider.AddProducer<FileCreatedMessage>(typeof(FileCreatedMessage).FullName);
								rider.AddProducer<FileModifiedMessage>(typeof(FileModifiedMessage).FullName);

								rider.UsingKafka(
									(context, k) =>
									{
										k.ClientId = typeof(IInfrastructureMessagingAssemblyMarker).Namespace;
										k.Host(settings.ConnectionStrings.ServiceBus);

										// TODO: auto-register
										/*k.TopicEndpoint<KafkaMessage>(typeof(IInfrastructureMessagingAssemblyMarker).Namespace, typeof(IInfrastructureMessagingAssemblyMarker).Namespace, e =>
										{
											e.ConfigureConsumer<KafkaMessageConsumer>(context);
										});*/
										k.TopicEndpoint<HashComputedMessage>(typeof(HashComputedMessage).FullName, typeof(IInfrastructureMessagingAssemblyMarker).Namespace, e =>
										{
											e.ConfigureConsumer<HashComputedMessageConsumer>(context);
										});
									});

								services.AddScoped<IBus, KafkaBus>();
							});
						break;
					case MessageBrokerType.RabbitMQ:
						a.UsingRabbitMq(
							(host, cfg) =>
							{
								cfg.Host(new Uri(settings.ConnectionStrings.ServiceBus));
								cfg.ConfigureEndpoints(host, new DefaultEndpointNameFormatter(".", "", true));
							});
						break;
					default:
						throw new ArgumentOutOfRangeException(
							nameof(settings.MessageBroker),
							$"Unknown Message Broker: '{settings.MessageBroker}'");
				}
			});

		return services;
	}

	private class BusStub : IBus
	{
		private readonly ILogger<BusStub> _logger;

		public BusStub(ILogger<BusStub> logger)
		{
			_logger = logger;
		}

		public ConnectHandle ConnectPublishObserver(IPublishObserver observer)
		{
			throw new NotImplementedException();
		}

		public Task<ISendEndpoint> GetPublishSendEndpoint<T>() where T : class
		{
			throw new NotImplementedException();
		}

		public Task Publish<T>(T message, CancellationToken cancellationToken = new()) where T : class
		{
			_logger.LogWarning($"Message Bus is not enabled. The message '{typeof(T)}' has not been sent.");

			return Task.CompletedTask;
		}

		public Task Publish<T>(
			T message,
			IPipe<PublishContext<T>> publishPipe,
			CancellationToken cancellationToken = new()) where T : class
		{
			throw new NotImplementedException();
		}

		public Task Publish<T>(
			T message,
			IPipe<PublishContext> publishPipe,
			CancellationToken cancellationToken = new()) where T : class
		{
			throw new NotImplementedException();
		}

		public Task Publish(object message, CancellationToken cancellationToken = new())
		{
			throw new NotImplementedException();
		}

		public Task Publish(
			object message,
			IPipe<PublishContext> publishPipe,
			CancellationToken cancellationToken = new())
		{
			throw new NotImplementedException();
		}

		public Task Publish(object message, Type messageType, CancellationToken cancellationToken = new())
		{
			throw new NotImplementedException();
		}

		public Task Publish(
			object message,
			Type messageType,
			IPipe<PublishContext> publishPipe,
			CancellationToken cancellationToken = new())
		{
			throw new NotImplementedException();
		}

		public Task Publish<T>(object values, CancellationToken cancellationToken = new()) where T : class
		{
			throw new NotImplementedException();
		}

		public Task Publish<T>(
			object values,
			IPipe<PublishContext<T>> publishPipe,
			CancellationToken cancellationToken = new()) where T : class
		{
			throw new NotImplementedException();
		}

		public Task Publish<T>(
			object values,
			IPipe<PublishContext> publishPipe,
			CancellationToken cancellationToken = new()) where T : class
		{
			throw new NotImplementedException();
		}

		public ConnectHandle ConnectSendObserver(ISendObserver observer)
		{
			throw new NotImplementedException();
		}

		public Task<ISendEndpoint> GetSendEndpoint(Uri address)
		{
			throw new NotImplementedException();
		}

		public ConnectHandle ConnectConsumePipe<T>(IPipe<ConsumeContext<T>> pipe) where T : class
		{
			throw new NotImplementedException();
		}

		public ConnectHandle ConnectConsumePipe<T>(IPipe<ConsumeContext<T>> pipe, ConnectPipeOptions options)
			where T : class
		{
			throw new NotImplementedException();
		}

		public ConnectHandle ConnectRequestPipe<T>(Guid requestId, IPipe<ConsumeContext<T>> pipe) where T : class
		{
			throw new NotImplementedException();
		}

		public ConnectHandle ConnectConsumeMessageObserver<T>(IConsumeMessageObserver<T> observer) where T : class
		{
			throw new NotImplementedException();
		}

		public ConnectHandle ConnectConsumeObserver(IConsumeObserver observer)
		{
			throw new NotImplementedException();
		}

		public ConnectHandle ConnectReceiveObserver(IReceiveObserver observer)
		{
			throw new NotImplementedException();
		}

		public ConnectHandle ConnectReceiveEndpointObserver(IReceiveEndpointObserver observer)
		{
			throw new NotImplementedException();
		}

		public ConnectHandle ConnectEndpointConfigurationObserver(IEndpointConfigurationObserver observer)
		{
			throw new NotImplementedException();
		}

		public HostReceiveEndpointHandle ConnectReceiveEndpoint(
			IEndpointDefinition definition,
			IEndpointNameFormatter? endpointNameFormatter = null,
			Action<IReceiveEndpointConfigurator>? configureEndpoint = null)
		{
			throw new NotImplementedException();
		}

		public HostReceiveEndpointHandle ConnectReceiveEndpoint(
			string queueName,
			Action<IReceiveEndpointConfigurator>? configureEndpoint = null)
		{
			throw new NotImplementedException();
		}

		public void Probe(ProbeContext context)
		{
			throw new NotImplementedException();
		}

		public Uri Address { get; }

		public IBusTopology Topology { get; }
	}

	private class KafkaBus : IBus
	{
		private readonly ILogger<KafkaBus> _logger;
		private readonly IServiceProvider _serviceProvider;

		public KafkaBus(IServiceProvider serviceProvider, ILogger<KafkaBus> logger)
		{
			_serviceProvider = serviceProvider;
			_logger = logger;
		}

		public ConnectHandle ConnectPublishObserver(IPublishObserver observer)
		{
			throw new NotImplementedException();
		}

		public Task<ISendEndpoint> GetPublishSendEndpoint<T>() where T : class
		{
			throw new NotImplementedException();
		}

		public async Task Publish<T>(T message, CancellationToken cancellationToken = new()) where T : class
		{
			var topicProducer = _serviceProvider.GetRequiredService<ITopicProducer<T>>();
			await topicProducer.Produce(message, cancellationToken);
		}

		public Task Publish<T>(
			T message,
			IPipe<PublishContext<T>> publishPipe,
			CancellationToken cancellationToken = new()) where T : class
		{
			throw new NotImplementedException();
		}

		public Task Publish<T>(
			T message,
			IPipe<PublishContext> publishPipe,
			CancellationToken cancellationToken = new()) where T : class
		{
			throw new NotImplementedException();
		}

		public Task Publish(object message, CancellationToken cancellationToken = new())
		{
			throw new NotImplementedException();
		}

		public Task Publish(
			object message,
			IPipe<PublishContext> publishPipe,
			CancellationToken cancellationToken = new())
		{
			throw new NotImplementedException();
		}

		public Task Publish(object message, Type messageType, CancellationToken cancellationToken = new())
		{
			throw new NotImplementedException();
		}

		public Task Publish(
			object message,
			Type messageType,
			IPipe<PublishContext> publishPipe,
			CancellationToken cancellationToken = new())
		{
			throw new NotImplementedException();
		}

		public Task Publish<T>(object values, CancellationToken cancellationToken = new()) where T : class
		{
			throw new NotImplementedException();
		}

		public Task Publish<T>(
			object values,
			IPipe<PublishContext<T>> publishPipe,
			CancellationToken cancellationToken = new()) where T : class
		{
			throw new NotImplementedException();
		}

		public Task Publish<T>(
			object values,
			IPipe<PublishContext> publishPipe,
			CancellationToken cancellationToken = new()) where T : class
		{
			throw new NotImplementedException();
		}

		public ConnectHandle ConnectSendObserver(ISendObserver observer)
		{
			throw new NotImplementedException();
		}

		public Task<ISendEndpoint> GetSendEndpoint(Uri address)
		{
			throw new NotImplementedException();
		}

		public ConnectHandle ConnectConsumePipe<T>(IPipe<ConsumeContext<T>> pipe) where T : class
		{
			throw new NotImplementedException();
		}

		public ConnectHandle ConnectConsumePipe<T>(IPipe<ConsumeContext<T>> pipe, ConnectPipeOptions options)
			where T : class
		{
			throw new NotImplementedException();
		}

		public ConnectHandle ConnectRequestPipe<T>(Guid requestId, IPipe<ConsumeContext<T>> pipe) where T : class
		{
			throw new NotImplementedException();
		}

		public ConnectHandle ConnectConsumeMessageObserver<T>(IConsumeMessageObserver<T> observer) where T : class
		{
			throw new NotImplementedException();
		}

		public ConnectHandle ConnectConsumeObserver(IConsumeObserver observer)
		{
			throw new NotImplementedException();
		}

		public ConnectHandle ConnectReceiveObserver(IReceiveObserver observer)
		{
			throw new NotImplementedException();
		}

		public ConnectHandle ConnectReceiveEndpointObserver(IReceiveEndpointObserver observer)
		{
			throw new NotImplementedException();
		}

		public ConnectHandle ConnectEndpointConfigurationObserver(IEndpointConfigurationObserver observer)
		{
			throw new NotImplementedException();
		}

		public HostReceiveEndpointHandle ConnectReceiveEndpoint(
			IEndpointDefinition definition,
			IEndpointNameFormatter? endpointNameFormatter = null,
			Action<IReceiveEndpointConfigurator>? configureEndpoint = null)
		{
			throw new NotImplementedException();
		}

		public HostReceiveEndpointHandle ConnectReceiveEndpoint(
			string queueName,
			Action<IReceiveEndpointConfigurator>? configureEndpoint = null)
		{
			throw new NotImplementedException();
		}

		public void Probe(ProbeContext context)
		{
			throw new NotImplementedException();
		}

		public Uri Address { get; }

		public IBusTopology Topology { get; }
	}
}