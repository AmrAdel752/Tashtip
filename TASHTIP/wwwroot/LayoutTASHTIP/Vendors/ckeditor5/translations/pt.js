>
      </summary>
      <param name="container">An object implementing the <see cref="T:System.ComponentModel.IContainer"/> interface which specifies the owner container of a <see cref="T:DevExpress.XtraPrinting.Link"/> class instance.</param>
    </member>
    <member name="M:DevExpress.XtraPrinting.Link.CanHandleCommand(DevExpress.XtraPrinting.PrintingSystemCommand,DevExpress.XtraPrinting.IPrintControl)">
      <summary>
        <para>Indicates whether or not the specified Printing System command can be handled.</para>
      </summary>
      <param name="command">A <see cref="T:DevExpress.XtraPrinting.PrintingSystemCommand"/> enumeration value that specifies the command.</param>
      <param name="printControl">An object implementing the <see cref="T:DevExpress.XtraPrinting.IPrintControl"/> interface that specifies the print control (most typically, it is a <see cref="T:DevExpress.XtraPrinting.Control.PrintControl"/> class instance).</param>
      <returns>true if the command can be handled; otherwise, false.</returns>
    </member>
    <member name="M:DevExpress.XtraPrinting.Link.CreateDocument(DevExpress.XtraPrinting.PrintingSystem)">
      <summary>
        <para>Generates a report using the specified PrintingSystem.</para>
      </summary>
      <param name="ps">A <see cref="T:DevExpress.XtraPrinting.PrintingSystem"/> class instance, specifying the printing system of the link.</param>
    </member>
    <member name="M:DevExpress.XtraPrinting.Link.HandleCommand(DevExpress.XtraPrinting.PrintingSystemCommand,System.Object[],DevExpress.XtraPrinting.IPrintControl,System.Boolea