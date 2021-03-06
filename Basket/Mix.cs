// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: Mix.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Basket {

  /// <summary>Holder for reflection information generated from Mix.proto</summary>
  public static partial class MixReflection {

    #region Descriptor
    /// <summary>File descriptor for Mix.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static MixReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "CglNaXgucHJvdG8SBkJhc2tldBoLSW1hZ2UucHJvdG8aD0ltYWdlRmlsZS5w",
            "cm90bxoUU2ltcGxlUmF3SW1hZ2UucHJvdG8ijgEKA01peBIeCgVJbWFnZRgB",
            "IAEoCzINLkJhc2tldC5JbWFnZUgAEiYKCUltYWdlRmlsZRgCIAEoCzIRLkJh",
            "c2tldC5JbWFnZUZpbGVIABIwCg5TaW1wbGVSYXdJbWFnZRgDIAEoCzIWLkJh",
            "c2tldC5TaW1wbGVSYXdJbWFnZUgAQg0KC0Zvcm1hdE9uZU9mYgZwcm90bzM="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::Basket.ImageReflection.Descriptor, global::Basket.ImageFileReflection.Descriptor, global::Basket.SimpleRawImageReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::Basket.Mix), global::Basket.Mix.Parser, new[]{ "Image", "ImageFile", "SimpleRawImage" }, new[]{ "FormatOneOf" }, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class Mix : pb::IMessage<Mix> {
    private static readonly pb::MessageParser<Mix> _parser = new pb::MessageParser<Mix>(() => new Mix());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<Mix> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Basket.MixReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Mix() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Mix(Mix other) : this() {
      switch (other.FormatOneOfCase) {
        case FormatOneOfOneofCase.Image:
          Image = other.Image.Clone();
          break;
        case FormatOneOfOneofCase.ImageFile:
          ImageFile = other.ImageFile.Clone();
          break;
        case FormatOneOfOneofCase.SimpleRawImage:
          SimpleRawImage = other.SimpleRawImage.Clone();
          break;
      }

      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Mix Clone() {
      return new Mix(this);
    }

    /// <summary>Field number for the "Image" field.</summary>
    public const int ImageFieldNumber = 1;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Basket.Image Image {
      get { return formatOneOfCase_ == FormatOneOfOneofCase.Image ? (global::Basket.Image) formatOneOf_ : null; }
      set {
        formatOneOf_ = value;
        formatOneOfCase_ = value == null ? FormatOneOfOneofCase.None : FormatOneOfOneofCase.Image;
      }
    }

    /// <summary>Field number for the "ImageFile" field.</summary>
    public const int ImageFileFieldNumber = 2;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Basket.ImageFile ImageFile {
      get { return formatOneOfCase_ == FormatOneOfOneofCase.ImageFile ? (global::Basket.ImageFile) formatOneOf_ : null; }
      set {
        formatOneOf_ = value;
        formatOneOfCase_ = value == null ? FormatOneOfOneofCase.None : FormatOneOfOneofCase.ImageFile;
      }
    }

    /// <summary>Field number for the "SimpleRawImage" field.</summary>
    public const int SimpleRawImageFieldNumber = 3;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Basket.SimpleRawImage SimpleRawImage {
      get { return formatOneOfCase_ == FormatOneOfOneofCase.SimpleRawImage ? (global::Basket.SimpleRawImage) formatOneOf_ : null; }
      set {
        formatOneOf_ = value;
        formatOneOfCase_ = value == null ? FormatOneOfOneofCase.None : FormatOneOfOneofCase.SimpleRawImage;
      }
    }

    private object formatOneOf_;
    /// <summary>Enum of possible cases for the "FormatOneOf" oneof.</summary>
    public enum FormatOneOfOneofCase {
      None = 0,
      Image = 1,
      ImageFile = 2,
      SimpleRawImage = 3,
    }
    private FormatOneOfOneofCase formatOneOfCase_ = FormatOneOfOneofCase.None;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public FormatOneOfOneofCase FormatOneOfCase {
      get { return formatOneOfCase_; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void ClearFormatOneOf() {
      formatOneOfCase_ = FormatOneOfOneofCase.None;
      formatOneOf_ = null;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as Mix);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(Mix other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (!object.Equals(Image, other.Image)) return false;
      if (!object.Equals(ImageFile, other.ImageFile)) return false;
      if (!object.Equals(SimpleRawImage, other.SimpleRawImage)) return false;
      if (FormatOneOfCase != other.FormatOneOfCase) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (formatOneOfCase_ == FormatOneOfOneofCase.Image) hash ^= Image.GetHashCode();
      if (formatOneOfCase_ == FormatOneOfOneofCase.ImageFile) hash ^= ImageFile.GetHashCode();
      if (formatOneOfCase_ == FormatOneOfOneofCase.SimpleRawImage) hash ^= SimpleRawImage.GetHashCode();
      hash ^= (int) formatOneOfCase_;
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (formatOneOfCase_ == FormatOneOfOneofCase.Image) {
        output.WriteRawTag(10);
        output.WriteMessage(Image);
      }
      if (formatOneOfCase_ == FormatOneOfOneofCase.ImageFile) {
        output.WriteRawTag(18);
        output.WriteMessage(ImageFile);
      }
      if (formatOneOfCase_ == FormatOneOfOneofCase.SimpleRawImage) {
        output.WriteRawTag(26);
        output.WriteMessage(SimpleRawImage);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (formatOneOfCase_ == FormatOneOfOneofCase.Image) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Image);
      }
      if (formatOneOfCase_ == FormatOneOfOneofCase.ImageFile) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(ImageFile);
      }
      if (formatOneOfCase_ == FormatOneOfOneofCase.SimpleRawImage) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(SimpleRawImage);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(Mix other) {
      if (other == null) {
        return;
      }
      switch (other.FormatOneOfCase) {
        case FormatOneOfOneofCase.Image:
          if (Image == null) {
            Image = new global::Basket.Image();
          }
          Image.MergeFrom(other.Image);
          break;
        case FormatOneOfOneofCase.ImageFile:
          if (ImageFile == null) {
            ImageFile = new global::Basket.ImageFile();
          }
          ImageFile.MergeFrom(other.ImageFile);
          break;
        case FormatOneOfOneofCase.SimpleRawImage:
          if (SimpleRawImage == null) {
            SimpleRawImage = new global::Basket.SimpleRawImage();
          }
          SimpleRawImage.MergeFrom(other.SimpleRawImage);
          break;
      }

      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 10: {
            global::Basket.Image subBuilder = new global::Basket.Image();
            if (formatOneOfCase_ == FormatOneOfOneofCase.Image) {
              subBuilder.MergeFrom(Image);
            }
            input.ReadMessage(subBuilder);
            Image = subBuilder;
            break;
          }
          case 18: {
            global::Basket.ImageFile subBuilder = new global::Basket.ImageFile();
            if (formatOneOfCase_ == FormatOneOfOneofCase.ImageFile) {
              subBuilder.MergeFrom(ImageFile);
            }
            input.ReadMessage(subBuilder);
            ImageFile = subBuilder;
            break;
          }
          case 26: {
            global::Basket.SimpleRawImage subBuilder = new global::Basket.SimpleRawImage();
            if (formatOneOfCase_ == FormatOneOfOneofCase.SimpleRawImage) {
              subBuilder.MergeFrom(SimpleRawImage);
            }
            input.ReadMessage(subBuilder);
            SimpleRawImage = subBuilder;
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code
