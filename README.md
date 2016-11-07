# XDSDotNet

## Sample Code

### ITI-41: Provide And Register Document Set

	// using XDSDotNet;
	// using System.ServiceModel;
	// using System.ServiceModel.Channels;

	var documentContents = new byte[] { 1, 2, 3, 4};
	var provideAndRegisterDocumentSet = new ProvideAndRegisterDocumentSet_ITI41(
		"patientid", 
		"sourceid", 
		"Document Name", 
		"mime/type", documentContents
	);

	provideAndRegisterDocumentSet.AddSubmissionSetClassification(new XDSClassification { 
		ClassificationScheme = XDSStrings.CLASSIFICATIONSCHEME_SUBMISSIONSET_CONTENTTYPECODE, 
		NodeRepresentation = "...", 
		Name = "..."
	});

	// ... add all required document classifications
	provideAndRegisterDocumentSet.AddDocumentClassification(new XDSClassification {
		ClassificationScheme = XDSStrings.CLASSIFICATIONSCHEME_TYPECODE,
		NodeRepresentation = "...",
		Name = "..."
	});

	// ... add slots
	provideAndRegisterDocumentSet.AddDocumentSlot(new XDSSlot {
		Name = "creationTime",
		Value = "..."
	});

	// ... set authorPerson
	provideAndRegisterDocumentSet.AuthorPerson = (new XCN { FirstName = "...", LastName = "..." }).Serialize();

	// ... and optionally authorInstitutions
	provideAndRegisterDocumentSet.AuthorInstitutions = new[] { (new XON { InstitutionName = "..."}).Serialize() };

	// the soap request body, ready for execution
	var requestBody = provideAndRegisterDocumentSet.CreateRequestBody();
	var requestMessage = Message.CreateMessage(MessageVersion.Soap12WSAddressing10, "urn:ihe:iti:2007:ProvideAndRegisterDocumentSet-b", requestBody);

	// create WCF channel
	var channelFactory = new ChannelFactory<IProvideAndRegisterDocumentSetITI41>("wcf endpoint configuration name");
	var channel = channelFactory.CreateChannel();

	// execute web service call
	var response = channel.ProvideAndRegisterDocumentSet(requestMessage);
	var responseBody = XElement.ReadFrom(response.GetReaderAtBodyContents()) as XElement;