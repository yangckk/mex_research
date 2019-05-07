// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: road.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
/// <summary>Holder for reflection information generated from road.proto</summary>
public static partial class RoadReflection {

  #region Descriptor
  /// <summary>File descriptor for road.proto</summary>
  public static pbr::FileDescriptor Descriptor {
    get { return descriptor; }
  }
  private static pbr::FileDescriptor descriptor;

  static RoadReflection() {
    byte[] descriptorData = global::System.Convert.FromBase64String(
        string.Concat(
          "Cgpyb2FkLnByb3RvGh9nb29nbGUvcHJvdG9idWYvdGltZXN0YW1wLnByb3Rv",
          "ImIKD1Bvc2l0aW9uUmVxdWVzdBIJCgF4GAEgASgCEgkKAXkYAiABKAISCQoB",
          "ehgDIAEoAhIuCgpjbGllbnRUaW1lGAQgASgLMhouZ29vZ2xlLnByb3RvYnVm",
          "LlRpbWVzdGFtcCJXCg1Qb3NpdGlvblJlcGx5EgsKA3BvcxgBIAEoCRIWCg5h",
          "Y3R1YXRpb25Gb3JjZRgCIAEoAhINCgVkVGltZRgDIAEoAxISCgpzZXJ2ZXJU",
          "aW1lGAQgASgJMj4KCFBvc2l0aW9uEjIKDFNlbmRQb3NpdGlvbhIQLlBvc2l0",
          "aW9uUmVxdWVzdBoOLlBvc2l0aW9uUmVwbHkiAGIGcHJvdG8z"));
    descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
        new pbr::FileDescriptor[] { global::Google.Protobuf.WellKnownTypes.TimestampReflection.Descriptor, },
        new pbr::GeneratedClrTypeInfo(null, new pbr::GeneratedClrTypeInfo[] {
          new pbr::GeneratedClrTypeInfo(typeof(global::PositionRequest), global::PositionRequest.Parser, new[]{ "X", "Y", "Z", "ClientTime" }, null, null, null),
          new pbr::GeneratedClrTypeInfo(typeof(global::PositionReply), global::PositionReply.Parser, new[]{ "Pos", "ActuationForce", "DTime", "ServerTime" }, null, null, null)
        }));
  }
  #endregion

}
#region Messages
/// <summary>
/// The request containing positional data of block
/// </summary>
public sealed partial class PositionRequest : pb::IMessage<PositionRequest> {
  private static readonly pb::MessageParser<PositionRequest> _parser = new pb::MessageParser<PositionRequest>(() => new PositionRequest());
  private pb::UnknownFieldSet _unknownFields;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static pb::MessageParser<PositionRequest> Parser { get { return _parser; } }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static pbr::MessageDescriptor Descriptor {
    get { return global::RoadReflection.Descriptor.MessageTypes[0]; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  pbr::MessageDescriptor pb::IMessage.Descriptor {
    get { return Descriptor; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public PositionRequest() {
    OnConstruction();
  }

  partial void OnConstruction();

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public PositionRequest(PositionRequest other) : this() {
    x_ = other.x_;
    y_ = other.y_;
    z_ = other.z_;
    clientTime_ = other.clientTime_ != null ? other.clientTime_.Clone() : null;
    _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public PositionRequest Clone() {
    return new PositionRequest(this);
  }

  /// <summary>Field number for the "x" field.</summary>
  public const int XFieldNumber = 1;
  private float x_;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public float X {
    get { return x_; }
    set {
      x_ = value;
    }
  }

  /// <summary>Field number for the "y" field.</summary>
  public const int YFieldNumber = 2;
  private float y_;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public float Y {
    get { return y_; }
    set {
      y_ = value;
    }
  }

  /// <summary>Field number for the "z" field.</summary>
  public const int ZFieldNumber = 3;
  private float z_;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public float Z {
    get { return z_; }
    set {
      z_ = value;
    }
  }

  /// <summary>Field number for the "clientTime" field.</summary>
  public const int ClientTimeFieldNumber = 4;
  private global::Google.Protobuf.WellKnownTypes.Timestamp clientTime_;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public global::Google.Protobuf.WellKnownTypes.Timestamp ClientTime {
    get { return clientTime_; }
    set {
      clientTime_ = value;
    }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override bool Equals(object other) {
    return Equals(other as PositionRequest);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public bool Equals(PositionRequest other) {
    if (ReferenceEquals(other, null)) {
      return false;
    }
    if (ReferenceEquals(other, this)) {
      return true;
    }
    if (!pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.Equals(X, other.X)) return false;
    if (!pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.Equals(Y, other.Y)) return false;
    if (!pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.Equals(Z, other.Z)) return false;
    if (!object.Equals(ClientTime, other.ClientTime)) return false;
    return Equals(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override int GetHashCode() {
    int hash = 1;
    if (X != 0F) hash ^= pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.GetHashCode(X);
    if (Y != 0F) hash ^= pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.GetHashCode(Y);
    if (Z != 0F) hash ^= pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.GetHashCode(Z);
    if (clientTime_ != null) hash ^= ClientTime.GetHashCode();
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
    if (X != 0F) {
      output.WriteRawTag(13);
      output.WriteFloat(X);
    }
    if (Y != 0F) {
      output.WriteRawTag(21);
      output.WriteFloat(Y);
    }
    if (Z != 0F) {
      output.WriteRawTag(29);
      output.WriteFloat(Z);
    }
    if (clientTime_ != null) {
      output.WriteRawTag(34);
      output.WriteMessage(ClientTime);
    }
    if (_unknownFields != null) {
      _unknownFields.WriteTo(output);
    }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public int CalculateSize() {
    int size = 0;
    if (X != 0F) {
      size += 1 + 4;
    }
    if (Y != 0F) {
      size += 1 + 4;
    }
    if (Z != 0F) {
      size += 1 + 4;
    }
    if (clientTime_ != null) {
      size += 1 + pb::CodedOutputStream.ComputeMessageSize(ClientTime);
    }
    if (_unknownFields != null) {
      size += _unknownFields.CalculateSize();
    }
    return size;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void MergeFrom(PositionRequest other) {
    if (other == null) {
      return;
    }
    if (other.X != 0F) {
      X = other.X;
    }
    if (other.Y != 0F) {
      Y = other.Y;
    }
    if (other.Z != 0F) {
      Z = other.Z;
    }
    if (other.clientTime_ != null) {
      if (clientTime_ == null) {
        clientTime_ = new global::Google.Protobuf.WellKnownTypes.Timestamp();
      }
      ClientTime.MergeFrom(other.ClientTime);
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
        case 13: {
          X = input.ReadFloat();
          break;
        }
        case 21: {
          Y = input.ReadFloat();
          break;
        }
        case 29: {
          Z = input.ReadFloat();
          break;
        }
        case 34: {
          if (clientTime_ == null) {
            clientTime_ = new global::Google.Protobuf.WellKnownTypes.Timestamp();
          }
          input.ReadMessage(clientTime_);
          break;
        }
      }
    }
  }

}

/// <summary>
/// Reply containing either left or right
/// </summary>
public sealed partial class PositionReply : pb::IMessage<PositionReply> {
  private static readonly pb::MessageParser<PositionReply> _parser = new pb::MessageParser<PositionReply>(() => new PositionReply());
  private pb::UnknownFieldSet _unknownFields;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static pb::MessageParser<PositionReply> Parser { get { return _parser; } }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static pbr::MessageDescriptor Descriptor {
    get { return global::RoadReflection.Descriptor.MessageTypes[1]; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  pbr::MessageDescriptor pb::IMessage.Descriptor {
    get { return Descriptor; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public PositionReply() {
    OnConstruction();
  }

  partial void OnConstruction();

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public PositionReply(PositionReply other) : this() {
    pos_ = other.pos_;
    actuationForce_ = other.actuationForce_;
    dTime_ = other.dTime_;
    serverTime_ = other.serverTime_;
    _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public PositionReply Clone() {
    return new PositionReply(this);
  }

  /// <summary>Field number for the "pos" field.</summary>
  public const int PosFieldNumber = 1;
  private string pos_ = "";
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public string Pos {
    get { return pos_; }
    set {
      pos_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }
  }

  /// <summary>Field number for the "actuationForce" field.</summary>
  public const int ActuationForceFieldNumber = 2;
  private float actuationForce_;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public float ActuationForce {
    get { return actuationForce_; }
    set {
      actuationForce_ = value;
    }
  }

  /// <summary>Field number for the "dTime" field.</summary>
  public const int DTimeFieldNumber = 3;
  private long dTime_;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public long DTime {
    get { return dTime_; }
    set {
      dTime_ = value;
    }
  }

  /// <summary>Field number for the "serverTime" field.</summary>
  public const int ServerTimeFieldNumber = 4;
  private string serverTime_ = "";
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public string ServerTime {
    get { return serverTime_; }
    set {
      serverTime_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override bool Equals(object other) {
    return Equals(other as PositionReply);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public bool Equals(PositionReply other) {
    if (ReferenceEquals(other, null)) {
      return false;
    }
    if (ReferenceEquals(other, this)) {
      return true;
    }
    if (Pos != other.Pos) return false;
    if (!pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.Equals(ActuationForce, other.ActuationForce)) return false;
    if (DTime != other.DTime) return false;
    if (ServerTime != other.ServerTime) return false;
    return Equals(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override int GetHashCode() {
    int hash = 1;
    if (Pos.Length != 0) hash ^= Pos.GetHashCode();
    if (ActuationForce != 0F) hash ^= pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.GetHashCode(ActuationForce);
    if (DTime != 0L) hash ^= DTime.GetHashCode();
    if (ServerTime.Length != 0) hash ^= ServerTime.GetHashCode();
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
    if (Pos.Length != 0) {
      output.WriteRawTag(10);
      output.WriteString(Pos);
    }
    if (ActuationForce != 0F) {
      output.WriteRawTag(21);
      output.WriteFloat(ActuationForce);
    }
    if (DTime != 0L) {
      output.WriteRawTag(24);
      output.WriteInt64(DTime);
    }
    if (ServerTime.Length != 0) {
      output.WriteRawTag(34);
      output.WriteString(ServerTime);
    }
    if (_unknownFields != null) {
      _unknownFields.WriteTo(output);
    }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public int CalculateSize() {
    int size = 0;
    if (Pos.Length != 0) {
      size += 1 + pb::CodedOutputStream.ComputeStringSize(Pos);
    }
    if (ActuationForce != 0F) {
      size += 1 + 4;
    }
    if (DTime != 0L) {
      size += 1 + pb::CodedOutputStream.ComputeInt64Size(DTime);
    }
    if (ServerTime.Length != 0) {
      size += 1 + pb::CodedOutputStream.ComputeStringSize(ServerTime);
    }
    if (_unknownFields != null) {
      size += _unknownFields.CalculateSize();
    }
    return size;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void MergeFrom(PositionReply other) {
    if (other == null) {
      return;
    }
    if (other.Pos.Length != 0) {
      Pos = other.Pos;
    }
    if (other.ActuationForce != 0F) {
      ActuationForce = other.ActuationForce;
    }
    if (other.DTime != 0L) {
      DTime = other.DTime;
    }
    if (other.ServerTime.Length != 0) {
      ServerTime = other.ServerTime;
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
          Pos = input.ReadString();
          break;
        }
        case 21: {
          ActuationForce = input.ReadFloat();
          break;
        }
        case 24: {
          DTime = input.ReadInt64();
          break;
        }
        case 34: {
          ServerTime = input.ReadString();
          break;
        }
      }
    }
  }

}

#endregion


#endregion Designer generated code
