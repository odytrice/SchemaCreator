module Generator

open System
open System.Data.SqlClient
open Microsoft.SqlServer.Management.Smo
open Microsoft.SqlServer.Management.Common
open DGML


let Generate () =
    //Prompt for outputfile name and connection string
    let outputFile = "test.dgml"
    let connectionString = @"Data Source=(local);Initial Catalog=CBTService;Integrated Security=SSPI;"

    use conn = new SqlConnection(connectionString)
    conn.Open()
    let server = new Server(new ServerConnection(conn))
    let database = server.Databases.[conn.Database]

    let dgmlHelper = new DgmlHelper(outputFile)
    dgmlHelper.BeginElement("Nodes")
    for table in database.Tables do
        // Create Nodes              
        dgmlHelper.WriteNode(table.ID.ToString(), table.Schema + "." + table.Name, String.Empty);

    dgmlHelper.EndElement();

    dgmlHelper.BeginElement("Links");
    for table in database.Tables do
        // Create Links
        for key in table.ForeignKeys do
            let toTable = database.Tables.[key.ReferencedTable, key.ReferencedTableSchema]
            dgmlHelper.WriteLink(table.ID.ToString(), toTable.ID.ToString(), key.Name)

    dgmlHelper.EndElement();

    //Close the DGML document
    dgmlHelper.Close();

    //Open the DGML in Visual Studio
    System.Diagnostics.Process.Start(outputFile);
            