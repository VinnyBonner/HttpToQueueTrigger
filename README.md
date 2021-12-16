# HttpToQueueTrigger

Example of how to add a message to a Storage Queue from a HttpTrigger with the message being the HttpTrigger param passed in.

Add the Storage account connection string as a app setting name AzureWebJobsStorage
Create a new Queue on the Storage account named outqueue.
Trigger the HttpTrigger Function with the passed in param - http://{url}/api/HttpExample?directory={Param}
The HttpTrigger will check if the directory param was passed. If it was the param passed in it will add the param as a message to the storage queue.
The QueueTrigger will be triggered by the new message added in the outqueue and will log the param passed in to the httpTrigger.
