// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: road.proto

#ifndef PROTOBUF_INCLUDED_road_2eproto
#define PROTOBUF_INCLUDED_road_2eproto

#include <string>

#include <google/protobuf/stubs/common.h>

#if GOOGLE_PROTOBUF_VERSION < 3006001
#error This file was generated by a newer version of protoc which is
#error incompatible with your Protocol Buffer headers.  Please update
#error your headers.
#endif
#if 3006001 < GOOGLE_PROTOBUF_MIN_PROTOC_VERSION
#error This file was generated by an older version of protoc which is
#error incompatible with your Protocol Buffer headers.  Please
#error regenerate this file with a newer version of protoc.
#endif

#include <google/protobuf/io/coded_stream.h>
#include <google/protobuf/arena.h>
#include <google/protobuf/arenastring.h>
#include <google/protobuf/generated_message_table_driven.h>
#include <google/protobuf/generated_message_util.h>
#include <google/protobuf/inlined_string_field.h>
#include <google/protobuf/metadata.h>
#include <google/protobuf/message.h>
#include <google/protobuf/repeated_field.h>  // IWYU pragma: export
#include <google/protobuf/extension_set.h>  // IWYU pragma: export
#include <google/protobuf/unknown_field_set.h>
#include <google/protobuf/timestamp.pb.h>
// @@protoc_insertion_point(includes)
#define PROTOBUF_INTERNAL_EXPORT_protobuf_road_2eproto 

namespace protobuf_road_2eproto {
// Internal implementation detail -- do not use these members.
struct TableStruct {
  static const ::google::protobuf::internal::ParseTableField entries[];
  static const ::google::protobuf::internal::AuxillaryParseTableField aux[];
  static const ::google::protobuf::internal::ParseTable schema[2];
  static const ::google::protobuf::internal::FieldMetadata field_metadata[];
  static const ::google::protobuf::internal::SerializationTable serialization_table[];
  static const ::google::protobuf::uint32 offsets[];
};
void AddDescriptors();
}  // namespace protobuf_road_2eproto
class PositionReply;
class PositionReplyDefaultTypeInternal;
extern PositionReplyDefaultTypeInternal _PositionReply_default_instance_;
class PositionRequest;
class PositionRequestDefaultTypeInternal;
extern PositionRequestDefaultTypeInternal _PositionRequest_default_instance_;
namespace google {
namespace protobuf {
template<> ::PositionReply* Arena::CreateMaybeMessage<::PositionReply>(Arena*);
template<> ::PositionRequest* Arena::CreateMaybeMessage<::PositionRequest>(Arena*);
}  // namespace protobuf
}  // namespace google

// ===================================================================

class PositionRequest : public ::google::protobuf::Message /* @@protoc_insertion_point(class_definition:PositionRequest) */ {
 public:
  PositionRequest();
  virtual ~PositionRequest();

  PositionRequest(const PositionRequest& from);

  inline PositionRequest& operator=(const PositionRequest& from) {
    CopyFrom(from);
    return *this;
  }
  #if LANG_CXX11
  PositionRequest(PositionRequest&& from) noexcept
    : PositionRequest() {
    *this = ::std::move(from);
  }

  inline PositionRequest& operator=(PositionRequest&& from) noexcept {
    if (GetArenaNoVirtual() == from.GetArenaNoVirtual()) {
      if (this != &from) InternalSwap(&from);
    } else {
      CopyFrom(from);
    }
    return *this;
  }
  #endif
  static const ::google::protobuf::Descriptor* descriptor();
  static const PositionRequest& default_instance();

  static void InitAsDefaultInstance();  // FOR INTERNAL USE ONLY
  static inline const PositionRequest* internal_default_instance() {
    return reinterpret_cast<const PositionRequest*>(
               &_PositionRequest_default_instance_);
  }
  static constexpr int kIndexInFileMessages =
    0;

  void Swap(PositionRequest* other);
  friend void swap(PositionRequest& a, PositionRequest& b) {
    a.Swap(&b);
  }

  // implements Message ----------------------------------------------

  inline PositionRequest* New() const final {
    return CreateMaybeMessage<PositionRequest>(NULL);
  }

  PositionRequest* New(::google::protobuf::Arena* arena) const final {
    return CreateMaybeMessage<PositionRequest>(arena);
  }
  void CopyFrom(const ::google::protobuf::Message& from) final;
  void MergeFrom(const ::google::protobuf::Message& from) final;
  void CopyFrom(const PositionRequest& from);
  void MergeFrom(const PositionRequest& from);
  void Clear() final;
  bool IsInitialized() const final;

  size_t ByteSizeLong() const final;
  bool MergePartialFromCodedStream(
      ::google::protobuf::io::CodedInputStream* input) final;
  void SerializeWithCachedSizes(
      ::google::protobuf::io::CodedOutputStream* output) const final;
  ::google::protobuf::uint8* InternalSerializeWithCachedSizesToArray(
      bool deterministic, ::google::protobuf::uint8* target) const final;
  int GetCachedSize() const final { return _cached_size_.Get(); }

  private:
  void SharedCtor();
  void SharedDtor();
  void SetCachedSize(int size) const final;
  void InternalSwap(PositionRequest* other);
  private:
  inline ::google::protobuf::Arena* GetArenaNoVirtual() const {
    return NULL;
  }
  inline void* MaybeArenaPtr() const {
    return NULL;
  }
  public:

  ::google::protobuf::Metadata GetMetadata() const final;

  // nested types ----------------------------------------------------

  // accessors -------------------------------------------------------

  // .google.protobuf.Timestamp clientTime = 4;
  bool has_clienttime() const;
  void clear_clienttime();
  static const int kClientTimeFieldNumber = 4;
  private:
  const ::google::protobuf::Timestamp& _internal_clienttime() const;
  public:
  const ::google::protobuf::Timestamp& clienttime() const;
  ::google::protobuf::Timestamp* release_clienttime();
  ::google::protobuf::Timestamp* mutable_clienttime();
  void set_allocated_clienttime(::google::protobuf::Timestamp* clienttime);

  // float x = 1;
  void clear_x();
  static const int kXFieldNumber = 1;
  float x() const;
  void set_x(float value);

  // float y = 2;
  void clear_y();
  static const int kYFieldNumber = 2;
  float y() const;
  void set_y(float value);

  // float z = 3;
  void clear_z();
  static const int kZFieldNumber = 3;
  float z() const;
  void set_z(float value);

  // @@protoc_insertion_point(class_scope:PositionRequest)
 private:

  ::google::protobuf::internal::InternalMetadataWithArena _internal_metadata_;
  ::google::protobuf::Timestamp* clienttime_;
  float x_;
  float y_;
  float z_;
  mutable ::google::protobuf::internal::CachedSize _cached_size_;
  friend struct ::protobuf_road_2eproto::TableStruct;
};
// -------------------------------------------------------------------

class PositionReply : public ::google::protobuf::Message /* @@protoc_insertion_point(class_definition:PositionReply) */ {
 public:
  PositionReply();
  virtual ~PositionReply();

  PositionReply(const PositionReply& from);

  inline PositionReply& operator=(const PositionReply& from) {
    CopyFrom(from);
    return *this;
  }
  #if LANG_CXX11
  PositionReply(PositionReply&& from) noexcept
    : PositionReply() {
    *this = ::std::move(from);
  }

  inline PositionReply& operator=(PositionReply&& from) noexcept {
    if (GetArenaNoVirtual() == from.GetArenaNoVirtual()) {
      if (this != &from) InternalSwap(&from);
    } else {
      CopyFrom(from);
    }
    return *this;
  }
  #endif
  static const ::google::protobuf::Descriptor* descriptor();
  static const PositionReply& default_instance();

  static void InitAsDefaultInstance();  // FOR INTERNAL USE ONLY
  static inline const PositionReply* internal_default_instance() {
    return reinterpret_cast<const PositionReply*>(
               &_PositionReply_default_instance_);
  }
  static constexpr int kIndexInFileMessages =
    1;

  void Swap(PositionReply* other);
  friend void swap(PositionReply& a, PositionReply& b) {
    a.Swap(&b);
  }

  // implements Message ----------------------------------------------

  inline PositionReply* New() const final {
    return CreateMaybeMessage<PositionReply>(NULL);
  }

  PositionReply* New(::google::protobuf::Arena* arena) const final {
    return CreateMaybeMessage<PositionReply>(arena);
  }
  void CopyFrom(const ::google::protobuf::Message& from) final;
  void MergeFrom(const ::google::protobuf::Message& from) final;
  void CopyFrom(const PositionReply& from);
  void MergeFrom(const PositionReply& from);
  void Clear() final;
  bool IsInitialized() const final;

  size_t ByteSizeLong() const final;
  bool MergePartialFromCodedStream(
      ::google::protobuf::io::CodedInputStream* input) final;
  void SerializeWithCachedSizes(
      ::google::protobuf::io::CodedOutputStream* output) const final;
  ::google::protobuf::uint8* InternalSerializeWithCachedSizesToArray(
      bool deterministic, ::google::protobuf::uint8* target) const final;
  int GetCachedSize() const final { return _cached_size_.Get(); }

  private:
  void SharedCtor();
  void SharedDtor();
  void SetCachedSize(int size) const final;
  void InternalSwap(PositionReply* other);
  private:
  inline ::google::protobuf::Arena* GetArenaNoVirtual() const {
    return NULL;
  }
  inline void* MaybeArenaPtr() const {
    return NULL;
  }
  public:

  ::google::protobuf::Metadata GetMetadata() const final;

  // nested types ----------------------------------------------------

  // accessors -------------------------------------------------------

  // string pos = 1;
  void clear_pos();
  static const int kPosFieldNumber = 1;
  const ::std::string& pos() const;
  void set_pos(const ::std::string& value);
  #if LANG_CXX11
  void set_pos(::std::string&& value);
  #endif
  void set_pos(const char* value);
  void set_pos(const char* value, size_t size);
  ::std::string* mutable_pos();
  ::std::string* release_pos();
  void set_allocated_pos(::std::string* pos);

  // string serverTime = 4;
  void clear_servertime();
  static const int kServerTimeFieldNumber = 4;
  const ::std::string& servertime() const;
  void set_servertime(const ::std::string& value);
  #if LANG_CXX11
  void set_servertime(::std::string&& value);
  #endif
  void set_servertime(const char* value);
  void set_servertime(const char* value, size_t size);
  ::std::string* mutable_servertime();
  ::std::string* release_servertime();
  void set_allocated_servertime(::std::string* servertime);

  // int64 dTime = 3;
  void clear_dtime();
  static const int kDTimeFieldNumber = 3;
  ::google::protobuf::int64 dtime() const;
  void set_dtime(::google::protobuf::int64 value);

  // float actuationForce = 2;
  void clear_actuationforce();
  static const int kActuationForceFieldNumber = 2;
  float actuationforce() const;
  void set_actuationforce(float value);

  // @@protoc_insertion_point(class_scope:PositionReply)
 private:

  ::google::protobuf::internal::InternalMetadataWithArena _internal_metadata_;
  ::google::protobuf::internal::ArenaStringPtr pos_;
  ::google::protobuf::internal::ArenaStringPtr servertime_;
  ::google::protobuf::int64 dtime_;
  float actuationforce_;
  mutable ::google::protobuf::internal::CachedSize _cached_size_;
  friend struct ::protobuf_road_2eproto::TableStruct;
};
// ===================================================================


// ===================================================================

#ifdef __GNUC__
  #pragma GCC diagnostic push
  #pragma GCC diagnostic ignored "-Wstrict-aliasing"
#endif  // __GNUC__
// PositionRequest

// float x = 1;
inline void PositionRequest::clear_x() {
  x_ = 0;
}
inline float PositionRequest::x() const {
  // @@protoc_insertion_point(field_get:PositionRequest.x)
  return x_;
}
inline void PositionRequest::set_x(float value) {
  
  x_ = value;
  // @@protoc_insertion_point(field_set:PositionRequest.x)
}

// float y = 2;
inline void PositionRequest::clear_y() {
  y_ = 0;
}
inline float PositionRequest::y() const {
  // @@protoc_insertion_point(field_get:PositionRequest.y)
  return y_;
}
inline void PositionRequest::set_y(float value) {
  
  y_ = value;
  // @@protoc_insertion_point(field_set:PositionRequest.y)
}

// float z = 3;
inline void PositionRequest::clear_z() {
  z_ = 0;
}
inline float PositionRequest::z() const {
  // @@protoc_insertion_point(field_get:PositionRequest.z)
  return z_;
}
inline void PositionRequest::set_z(float value) {
  
  z_ = value;
  // @@protoc_insertion_point(field_set:PositionRequest.z)
}

// .google.protobuf.Timestamp clientTime = 4;
inline bool PositionRequest::has_clienttime() const {
  return this != internal_default_instance() && clienttime_ != NULL;
}
inline const ::google::protobuf::Timestamp& PositionRequest::_internal_clienttime() const {
  return *clienttime_;
}
inline const ::google::protobuf::Timestamp& PositionRequest::clienttime() const {
  const ::google::protobuf::Timestamp* p = clienttime_;
  // @@protoc_insertion_point(field_get:PositionRequest.clientTime)
  return p != NULL ? *p : *reinterpret_cast<const ::google::protobuf::Timestamp*>(
      &::google::protobuf::_Timestamp_default_instance_);
}
inline ::google::protobuf::Timestamp* PositionRequest::release_clienttime() {
  // @@protoc_insertion_point(field_release:PositionRequest.clientTime)
  
  ::google::protobuf::Timestamp* temp = clienttime_;
  clienttime_ = NULL;
  return temp;
}
inline ::google::protobuf::Timestamp* PositionRequest::mutable_clienttime() {
  
  if (clienttime_ == NULL) {
    auto* p = CreateMaybeMessage<::google::protobuf::Timestamp>(GetArenaNoVirtual());
    clienttime_ = p;
  }
  // @@protoc_insertion_point(field_mutable:PositionRequest.clientTime)
  return clienttime_;
}
inline void PositionRequest::set_allocated_clienttime(::google::protobuf::Timestamp* clienttime) {
  ::google::protobuf::Arena* message_arena = GetArenaNoVirtual();
  if (message_arena == NULL) {
    delete reinterpret_cast< ::google::protobuf::MessageLite*>(clienttime_);
  }
  if (clienttime) {
    ::google::protobuf::Arena* submessage_arena =
      reinterpret_cast<::google::protobuf::MessageLite*>(clienttime)->GetArena();
    if (message_arena != submessage_arena) {
      clienttime = ::google::protobuf::internal::GetOwnedMessage(
          message_arena, clienttime, submessage_arena);
    }
    
  } else {
    
  }
  clienttime_ = clienttime;
  // @@protoc_insertion_point(field_set_allocated:PositionRequest.clientTime)
}

// -------------------------------------------------------------------

// PositionReply

// string pos = 1;
inline void PositionReply::clear_pos() {
  pos_.ClearToEmptyNoArena(&::google::protobuf::internal::GetEmptyStringAlreadyInited());
}
inline const ::std::string& PositionReply::pos() const {
  // @@protoc_insertion_point(field_get:PositionReply.pos)
  return pos_.GetNoArena();
}
inline void PositionReply::set_pos(const ::std::string& value) {
  
  pos_.SetNoArena(&::google::protobuf::internal::GetEmptyStringAlreadyInited(), value);
  // @@protoc_insertion_point(field_set:PositionReply.pos)
}
#if LANG_CXX11
inline void PositionReply::set_pos(::std::string&& value) {
  
  pos_.SetNoArena(
    &::google::protobuf::internal::GetEmptyStringAlreadyInited(), ::std::move(value));
  // @@protoc_insertion_point(field_set_rvalue:PositionReply.pos)
}
#endif
inline void PositionReply::set_pos(const char* value) {
  GOOGLE_DCHECK(value != NULL);
  
  pos_.SetNoArena(&::google::protobuf::internal::GetEmptyStringAlreadyInited(), ::std::string(value));
  // @@protoc_insertion_point(field_set_char:PositionReply.pos)
}
inline void PositionReply::set_pos(const char* value, size_t size) {
  
  pos_.SetNoArena(&::google::protobuf::internal::GetEmptyStringAlreadyInited(),
      ::std::string(reinterpret_cast<const char*>(value), size));
  // @@protoc_insertion_point(field_set_pointer:PositionReply.pos)
}
inline ::std::string* PositionReply::mutable_pos() {
  
  // @@protoc_insertion_point(field_mutable:PositionReply.pos)
  return pos_.MutableNoArena(&::google::protobuf::internal::GetEmptyStringAlreadyInited());
}
inline ::std::string* PositionReply::release_pos() {
  // @@protoc_insertion_point(field_release:PositionReply.pos)
  
  return pos_.ReleaseNoArena(&::google::protobuf::internal::GetEmptyStringAlreadyInited());
}
inline void PositionReply::set_allocated_pos(::std::string* pos) {
  if (pos != NULL) {
    
  } else {
    
  }
  pos_.SetAllocatedNoArena(&::google::protobuf::internal::GetEmptyStringAlreadyInited(), pos);
  // @@protoc_insertion_point(field_set_allocated:PositionReply.pos)
}

// float actuationForce = 2;
inline void PositionReply::clear_actuationforce() {
  actuationforce_ = 0;
}
inline float PositionReply::actuationforce() const {
  // @@protoc_insertion_point(field_get:PositionReply.actuationForce)
  return actuationforce_;
}
inline void PositionReply::set_actuationforce(float value) {
  
  actuationforce_ = value;
  // @@protoc_insertion_point(field_set:PositionReply.actuationForce)
}

// int64 dTime = 3;
inline void PositionReply::clear_dtime() {
  dtime_ = GOOGLE_LONGLONG(0);
}
inline ::google::protobuf::int64 PositionReply::dtime() const {
  // @@protoc_insertion_point(field_get:PositionReply.dTime)
  return dtime_;
}
inline void PositionReply::set_dtime(::google::protobuf::int64 value) {
  
  dtime_ = value;
  // @@protoc_insertion_point(field_set:PositionReply.dTime)
}

// string serverTime = 4;
inline void PositionReply::clear_servertime() {
  servertime_.ClearToEmptyNoArena(&::google::protobuf::internal::GetEmptyStringAlreadyInited());
}
inline const ::std::string& PositionReply::servertime() const {
  // @@protoc_insertion_point(field_get:PositionReply.serverTime)
  return servertime_.GetNoArena();
}
inline void PositionReply::set_servertime(const ::std::string& value) {
  
  servertime_.SetNoArena(&::google::protobuf::internal::GetEmptyStringAlreadyInited(), value);
  // @@protoc_insertion_point(field_set:PositionReply.serverTime)
}
#if LANG_CXX11
inline void PositionReply::set_servertime(::std::string&& value) {
  
  servertime_.SetNoArena(
    &::google::protobuf::internal::GetEmptyStringAlreadyInited(), ::std::move(value));
  // @@protoc_insertion_point(field_set_rvalue:PositionReply.serverTime)
}
#endif
inline void PositionReply::set_servertime(const char* value) {
  GOOGLE_DCHECK(value != NULL);
  
  servertime_.SetNoArena(&::google::protobuf::internal::GetEmptyStringAlreadyInited(), ::std::string(value));
  // @@protoc_insertion_point(field_set_char:PositionReply.serverTime)
}
inline void PositionReply::set_servertime(const char* value, size_t size) {
  
  servertime_.SetNoArena(&::google::protobuf::internal::GetEmptyStringAlreadyInited(),
      ::std::string(reinterpret_cast<const char*>(value), size));
  // @@protoc_insertion_point(field_set_pointer:PositionReply.serverTime)
}
inline ::std::string* PositionReply::mutable_servertime() {
  
  // @@protoc_insertion_point(field_mutable:PositionReply.serverTime)
  return servertime_.MutableNoArena(&::google::protobuf::internal::GetEmptyStringAlreadyInited());
}
inline ::std::string* PositionReply::release_servertime() {
  // @@protoc_insertion_point(field_release:PositionReply.serverTime)
  
  return servertime_.ReleaseNoArena(&::google::protobuf::internal::GetEmptyStringAlreadyInited());
}
inline void PositionReply::set_allocated_servertime(::std::string* servertime) {
  if (servertime != NULL) {
    
  } else {
    
  }
  servertime_.SetAllocatedNoArena(&::google::protobuf::internal::GetEmptyStringAlreadyInited(), servertime);
  // @@protoc_insertion_point(field_set_allocated:PositionReply.serverTime)
}

#ifdef __GNUC__
  #pragma GCC diagnostic pop
#endif  // __GNUC__
// -------------------------------------------------------------------


// @@protoc_insertion_point(namespace_scope)


// @@protoc_insertion_point(global_scope)

#endif  // PROTOBUF_INCLUDED_road_2eproto
