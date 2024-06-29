# StatlerWaldorfCorp.ES-ProximityMonitor

The Proximity Monitor microservice application listens for ProximityDetected events, picks up the events from Queue, augments them, then sends the events out for dispatch to real-time messaging system.

This example illustrates how to create a monitor microsercie for an event stream emitted by the Event Processor service. Using an integrated Pubnub messaging, real-time update is sent to monitor screens. Here, nearby team members can recieve mobile notifications at the same time.

### Overview
Purpose: Demonstrates monitoring an event stream for proximity detection events.
Use Case: Useful for scenarios where proximity detection is critical and needs to be monitored in real-time. it can send a new message out on a real-time messaging system using PubNub.

Key Concepts
- Event Processor: Component responsible for picking event from Message Queue, proceses events and emit as streams.
- Event Stream: The continuous flow of events processed by the event processor.
- Monitor: Listens to the event stream and acts upon ProximityDetected events is emitted as message. It's a JS Web Browser HTML application that listen to push events/messages and displays on screen in realtime.

### Implementation
Design Considerations
- Multiple Event Processors: You can have multiple processors handling different aspects of your application.
- Multiple Outbound Streams: Each processor can emit various event streams.
- Multiple Monitors: Each stream can be monitored by one or more monitors, depending on the application's needs.


### Implementation
1. Define Event Processor: Create processors to handle specific tasks within your domain.
2. Emit Event Streams: Processors emit event streams based on the specific events they handle.
3. Create Monitors: Monitors listen to these streams and take appropriate actions when events are detected.

### Example Use Case

This example monitors proximity detection events to illustrate how you can set up and use monitors within your application. Depending on your application's domain and expected volume, you may need to design your event processors, streams, and monitors differently.                                                                