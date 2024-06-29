# StatlerWaldorfCorp.ES-ProximityMonitor

The Proximity Monitor microservice application listens for ProximityDetected events, picks up the events from Queue, augments them, then sends the events out for dispatch to real-time messaging system.

This example illustrates how to create a monitor microsercie for an event stream emitted by the Event Processor service. Using an integrated Pubnub messaging, real-time update is sent to monitor screens. Here, nearby team members can recieve mobile notifications at the same time.

### Overview
Purpose: Demonstrates monitoring an event stream for proximity detection events.
Use Case: Useful for scenarios where proximity detection is critical and needs to be monitored in real-time. it can send a new message out on a real-time messaging system using PubNub.

### Components
- Messaging Queue - RabbitMQ to pick event from queu
- Http service communication - Team Service to augment events
- Realtime messaging - PubNub tool to push realtime message as soon as available

> message queue vs http vs realtime

#### Key Concepts
- Event Processor: Component responsible for picking event from Message Queue, proceses events and emit as streams.
- Event Stream: The continuous flow of events processed by the event processor.
- Monitor: Listens to the event stream and acts upon ProximityDetected events is emitted as message. It's a JS Web Browser HTML application that listen to push events/messages and displays on screen in realtime.

### Implementation
Design Considerations
- Multiple Event Processors: You can have multiple processors handling different aspects of your application.
- Multiple Outbound Streams: Each processor can emit various event streams.
- Multiple Monitors: Each stream can be monitored by one or more monitors, depending on the application's needs.

1. Define Event Processor: Create processors to handle specific tasks within your domain.
2. Emit Event Streams: Processors emit event streams based on the specific events they handle.
3. Create Monitors: Monitors listen to these streams and take appropriate actions when events are detected.

### Real-time messaging
Real-time messaging tools like PubNub are essential for integrating and coordinating independent components in highly scalable, distributed, and eventually consistent systems, especially when these systems need to interact with client-facing UIs. Because they:
- provide communication channel over a single, long-lived connection allowing server to instantly push updates to connected client through channels.
- are designed to handle precisely this kind of scenario where a service needs to send updates to a client frontend app in real-time. Whether you use WebSockets, SignalR, PubNub, or another tool, you can achieve real-time communication effectively.

    - Reasons why HTTP POST from server to client is not effective
        - POST request from server means client should have endpoint which is definitel NOT ideal
        - HTTP is inherently a request-response protocol
        - Unnecessary network traffic and latency 
            - ...because each request-and-response(though ONLY POST but there's response side-effect) pair involves network round trips
        - Constantly opening and closing connections with HTTP POST requests consumes more server and network resources 
            - ...compared to a single, persistent connection used in protocols designed for real-time communication (e.g., WebSockets).


### Message Queue vs HTTP vs Real-time communication:
Each of these technologies serves different purposes and is chosen based on the specific requirements of the application.

1. HTTP
- Nature: Request-response model.
- Use Case: Synchronous communication where the client initiates requests and waits for responses.
- Pros:
    - Simple and widely used for variety of clients, including web browsers and mobile devices.
    - Suitable for RESTful APIs and web services.
- Cons:
    - Not suitable for real-time updates.
    - Requires polling or long-polling for near real-time updates, which is inefficient.
    - Stateless, each request is independent.

2. Message Queues
- Nature: Asynchronous communication.
- Use Case: Decoupling components, ensuring message delivery even if the receiver is temporarily unavailable.
- Pros:
    - Reliable and durable message delivery.
    - Supports complex routing and message persistence.
    - Can handle load balancing and message prioritization.
- Cons:
    - Adds complexity to the system.
    - Typically used for backend communication, not direct client interactions.

3. Real-Time Communication
- Nature: Persistent, bi-directional communication.
- Use Case: Real-time updates, instant messaging, live notifications, collaborative applications.
- Pros:
    - Low latency, immediate updates.
    - Bi-directional communication allows both client and server to push updates.
    - Efficient use of resources with a single persistent connection (e.g., WebSockets).
- Cons:
    - Requires more complex setup and infrastructure.
    - Persistent connections can be resource-intensive.

4. gRPC
- Nature: High-performance, primarily operates as a request-response model but with several advanced features that make it more efficient and versatile compared to traditional HTTP/REST. Improved version of HTTP/REST in short.
- Use Case: Efficient communication between microservices, supports streaming and real-time updates.
- Unlike HTTP which is:
    - Text-Based (typically uses JSON or XML), 
    - Stateles (each request is independent, ),
    - Widely Compatibility to all web browsers and most programming languages out of the box.
    - GRPC is:
        - Binary-Based - uses Protocol Buffers which are more efficient for serialization/deserialization.
        - Stateful Connections - leveraging HTTP/2 allows for persistent connections, reducing overhead for each request.
        - Supportd by various streaming models, enhancing real-time communication capabilities.
- Pros:
    - High performance and low latency.
    - Supports bidirectional streaming.
    - Strongly typed contracts with Protocol Buffers (protobuf).
    - Ideal for microservices architecture.
- Cons:
    - More complex to set up and manage compared to simple HTTP/REST.
    - Requires support for HTTP/2, which may not be available in all environments.
    - Steeper learning curve due to Protocol Buffers and gRPC-specific concepts.

*In Summary..*
- HTTP: Best for request-response interactions, not ideal for real-time updates.
- Message Queues: Best for asynchronous, decoupled communication between backend services.
- Real-Time Communication: Best for real-time, interactive applications requiring immediate updates.
- gRPC: Best for high-performance communication between microservices, supports real-time updates with streaming, and provides strong typing with Protocol Buffers.
