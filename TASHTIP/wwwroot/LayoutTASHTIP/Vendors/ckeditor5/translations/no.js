ystemCommand)">
      <summary>
        <para>Returns a value indicating whether or not the specified command is enabled.</para>
      </summary>
      <param name="command">A <see cref="T:DevExpress.XtraPrinting.PrintingSystemCommand"/> enumeration value.</param>
      <returns>true if the command is enabled; otherwise, false.</returns>
    </member>
    <member name="E:DevExpress.XtraPrinting.Control.PrintControl.PaintBookmarkArea">
      <summary>
        <para>Occurs after a selection border has been drawn around an element in Print Preview.</para>
      </summary>
    </member>
    <member name="E:DevExpress.XtraPrinting.Control.PrintControl.PaintMarkedBricksArea">
      <summary>
        <para>Occurs when the area of a marked brick is painted.</para>
      </summary>
    </member>
    <member name="P:DevExpress.XtraPrinting.Control.PrintControl.PrintingSystem">
      <summary>
        <para>Specifies the Printing System that is used to create document pages for a link or report opened in Print Preview.</para>
      </summary>
      <value>A <see cref="T:DevExpress.XtraPrinting.PrintingSystemBase"/> class descendant.</value>
    </member>
    <member name="M:DevExpress.XtraPrinting.Control.PrintControl.SetDocumentMapVisibility(System.Boolean)">
      <summary>
        <para>Specifies the visibility of the Document Map in Print Preview.</para>
      </summary>
      <param name="value">true, to display the Document Map; otherwise, false.</param>
    </member>
    <member name="M:DevExpress.XtraPrinting.Control.PrintControl.SetThumbnailsVisibility(System.Boolean)">
      <summary>
        <para>Specifies the visibility of the Thumbnails panel in Print Preview.</para>
      </summary>
      <param name="value">true, to display the Thumbnails panel; otherwise, false.</param>
    </member>
    <member name="M:DevExpress.XtraPrinting.Control.PrintControl.ShowBrick(DevExpress.XtraPrinting.Brick,DevExpress.XtraPrinting.Page)">
      <summary>
        <para>Navigates to the specified brick in a document opened in Print Preview.</para>
      </summary>
      <param name="brick">A <see cref="T:DevExpress.XtraPrinting.Brick"/> object, specifying the brick to navigate to.</param>
      <param name="page">A <see cref="T:DevExpress.XtraPrinting.Page"/> object, specifying the page at which the brick is located. When a brick occupies multiple pages, the focus will move to that part of the brick which is printed on the specified page.</param>
    </member>
    <member name="M:DevExpress.XtraPrinting.Control.PrintControl.ShowBrickCenter(DevExpress.XtraPrinting.Brick,DevExpress.XtraPrinting.Page)">
      <summary>
        <para>Locates the specified brick and displays it in the center of the visible <see cref="T:DevExpress.XtraPrinting.Control.PrintControl"/> area.</para>
      </summary>
      <param name="brick">A <see cref="T:DevExpress.XtraPrinting.Brick"/> object, specifying the brick to be located.</param>
      <param name="page">A <see cref="T:DevExpress.XtraPrinting.Page"/> object, specifying the document page at which the brick is located.</param>
    </member>
    <member name="M:DevExpress.XtraPrinting.Control.PrintControl.ShowFindControl">
      <summary>
        <para>Displays the Find panel enabling text search in a document.</para>
      </summary>
    </member>
    <member name="P:DevExpress.XtraPrinting.Control.PrintControl.ThumbnailsVisible">
      <summary>
        <para>Indicates whether or not the Thumbnails panel is visible in Print Preview.</para>
      </summary>
      <value>true if the Thumbnails panel is visible; otherwise, false.</value>
    </member>
    <member name="P:DevExpress.XtraPrinting.Control.PrintControl.UseAsyncDocumentCreation">
      <summary>
        <para>Gets or sets whether the <see cref="T:DevExpress.XtraPrinting.Preview.DocumentV