<para>Occurs when the mouse pointer moves over the region specified by a brick within the <see cref="T:DevExpress.XtraPrinting.Control.PrintControl"/>.</para>
      </summary>
    </member>
    <member name="E:DevExpress.XtraPrinting.Control.PrintControl.BrickUp">
      <summary>
        <para>Occurs when the mouse pointer is over the region specified by a brick within the <see cref="T:DevExpress.XtraPrinting.Control.PrintControl"/> and the mouse button is released.</para>
      </summary>
    </member>
    <member name="M:DevExpress.XtraPrinting.Control.PrintControl.CanExecCommand(DevExpress.XtraPrinting.PrintingSystemCommand)">
      <summary>
        <para>Checks whether or not the specified command can be executed.</para>
      </summary>
      <param name="command">A <see cref="T:DevExpress.XtraPrinting.PrintingSystemCommand"/> enumeration value.</param>
      <returns>true if the command can be executed; otherwise, false.</returns>
    </member>
    <member name="M:DevExpress.XtraPrinting.Control.PrintControl.CloseFindControl">
      <summary>
        <para>Hides the Find panel enabling text search in a document.</para>
      </summary>
    </member>
    <member name="E:DevExpress.XtraPrinting.Control.PrintControl.CommandChanged">
      <summary>
        <para>Occurs after the status of a printing system command has been changed.</para>
      </summary>
    </member>
    <member name="M:DevExpress.XtraPrinting.Control.PrintControl.DisableExportModeValues(System.Object[])">
      <summary>
        <para>Removes the specified export modes from the list of export options available in Print Preview.</para>
      </summary>
      <param name="disableValues">An array of export modes to remove.</param>
    </member>
    <member name="P:DevExpress.XtraPrinting.Control.PrintControl.DockingViewStyle">
      <summary>
        <para>Specifies the View style for dock panels.</para>
      </summary>
      <value>An enumeration value that specifies the View style.</value>
    </member>
    <member name="P:DevExpress.XtraPrinting.Control.PrintControl.DockManager">
      <summary>
        <para>Provides access to the <see cref="T:DevExpress.XtraBars.Docking.DockManager"/>, specifying the settings of dock panels in Print Preview.</para>
      </summary>
      <value>A <see cref="T:DevExpress.XtraBars.Docking.DockManager"/> object.</value>
    </member>
    <member name="P:DevExpress.XtraPrinting.Control.PrintControl.DocumentHasBookmarks">
      <summary>
        <para>Indicates whether or not a <see cref="T:DevExpress.XtraPrinting.Document"/> opened in Print Preview has any bookmarks.</para>
      </summary>
      <value>true if the document has bookmarks assigned to its elements; otherwise, false.</value>
    </member>
    <member name="P:DevExpress.XtraPrinting.Control.PrintControl.DocumentMapVisible">
      <summary>
        <para>Indicates whether or not the Document Map is visible in Print Preview.</para>
      </summary>
      <value>true if the Document Map is visible; otherwise, false.</value>
    </member>
    <member name="P:DevExpress.XtraPrinting.Control.PrintControl.EditingFieldsHighlighted">
      <summary>
        <para>Indicates whether the editing fields are highlighted in the document displayed in the Print Preview.</para>
      </summary>
      <value>true if the document has the editing fields highlighted; otherwise, false.</value>
    </member>
    <member name="M:DevExpress.XtraPrinting.Control.PrintControl.ExecCommand(DevExpress.XtraPrinting.PrintingSystemCommand)">
      <summary>
        <para>Executes the specified command without any parameters.</para>
      </summary>
      <param name="command">A <see cref="T:DevExpress.XtraPrinting.PrintingSystemCommand"/> enumeration value, specifying the command to execute.</param>
    </member>
    <member name="M:DevExpress.XtraPrinting.Control.PrintControl.ExecCommand(DevEx