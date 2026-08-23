<?xml version="1.0" encoding="utf-8"?>
<doc xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <assembly>
    <name>DevExpress.XtraPrinting.v20.2</name>
  </assembly>
  <members>
    <member name="N:DevExpress.XtraPrinting">
      <summary>
        <para>Contains classes that provide the basic functionality for the XtraPrinting library (bricks, pages, document).</para>
      </summary>
    </member>
    <member name="N:DevExpress.XtraPrinting.Control">
      <summary>
        <para>Contains classes that implement the PrintControl functionality.</para>
      </summary>
    </member>
    <member name="T:DevExpress.XtraPrinting.Control.AreaPaintEventArgs">
      <summary>
        <para>Provides data for the <see cref="E:DevExpress.XtraPrinting.Control.PrintControl.PaintMarkedBricksArea"/> event.</para>
      </summary>
    </member>
    <member name="P:DevExpress.XtraPrinting.Control.AreaPaintEventArgs.Area">
      <summary>
        <para>Gets the area around the element being painted.</para>
      </summary>
      <value>A <see cref="T:System.Drawing.RectangleF"/> structure.</value>
    </member>
    <member name="P:DevExpress.XtraPrinting.Control.AreaPaintEventArgs.Graphics">
      <summary>
        <para>Gets the drawing surface of the element being painted.</para>
      </summary>
      <value>A <see cref="T:System.Drawing.Graphics"/> object.</value>
    </member>
    <member name="T:DevExpress.XtraPrinting.Control.BrickEventArgs">
      <summary>
        <para>Provides data for all brick events within the XtraPrinting Library.</para>
      </summary>
    </member>
    <member name="M:DevExpress.XtraPrinting.Control.BrickEventArgs.#ctor(DevExpress.XtraPrinting.Brick)">
      <summary>
        <para>Initializes a new instance of the <see cref="T:DevExpress.XtraPrinting.Control.BrickEventArgs"/> class with the specified brick.</para>
      </summary>
      <param name="brick">A <see cref="T:DevExpress.XtraPrinting.Brick"/> value. This value is assigned to the <see cref="P:DevExpress.XtraPrinting.BrickEventArgsBase.Brick"/> property.</param>
    </member>
    <member name="M:DevExpress.XtraPrinting.Control.BrickEventArgs.#ctor(System.EventArgs,DevExpress.XtraPrinting.Brick,DevExpress.XtraPrinting.Page,System.Drawing.Rectangle,System.Single,System.Single)">
      <summary>
        <para>Initializes a new instance of the <see cref="T:DevExpress.XtraPrin