---
title: Oversized Sends
reviewed: 2019-02-27
component: ASB
versions: '[7,)'
tags:
- Azure
- Error Handling
redirects:
 - nservicebus/azure-service-bus/oversized-sends
 - transports/azure-service-bus/oversized-sends
---

include: legacy-asb-warning

The Azure Service Bus SDK attempts to prevent sending of messages that exceed the maximum message size supported by the SQS broker by calculating the size using the body of the message, however it does not take into account headers or overhead added by serialization performed in the SDK. As a result oversized messages may be sent to the broker and only reported after the message has been rejected and discarded by the Azure Service Bus broker.

The transport deals with this problem for a large part by performing an estimated size calculation that includes both body and headers as well as a percentage for padding as an attempt to compensate for additional message serialization performed by the SDK.

The recommended way to send message payloads which may exceed the transport message size limits is to use the NServiceBus [data bus feature](/nservicebus/messaging/databus/), however using the data bus feature is not always desirable, especially when exceeding the message size limits of Azure Service Bus is uncommon.

As an alternative to using the data bus feature, a custom implementation of `IHandleOversizedBrokeredMessages` can be configured

## Configuring an oversized brokered message handler

snippet: asb-configure-oversized-messages-handler

and registered using the `OversizedBrokeredMessageHandler` API.

snippet: asb-configure-oversized-messages-handler-config
