// Generated by the gRPC C++ plugin.
// If you make any local change, they will be lost.
// source: road.proto
#ifndef GRPC_road_2eproto__INCLUDED
#define GRPC_road_2eproto__INCLUDED

#include "road.pb.h"

#include <functional>
#include <grpc/impl/codegen/port_platform.h>
#include <grpcpp/impl/codegen/async_generic_service.h>
#include <grpcpp/impl/codegen/async_stream.h>
#include <grpcpp/impl/codegen/async_unary_call.h>
#include <grpcpp/impl/codegen/client_callback.h>
#include <grpcpp/impl/codegen/client_context.h>
#include <grpcpp/impl/codegen/completion_queue.h>
#include <grpcpp/impl/codegen/message_allocator.h>
#include <grpcpp/impl/codegen/method_handler.h>
#include <grpcpp/impl/codegen/proto_utils.h>
#include <grpcpp/impl/codegen/rpc_method.h>
#include <grpcpp/impl/codegen/server_callback.h>
#include <grpcpp/impl/codegen/server_callback_handlers.h>
#include <grpcpp/impl/codegen/server_context.h>
#include <grpcpp/impl/codegen/service_type.h>
#include <grpcpp/impl/codegen/status.h>
#include <grpcpp/impl/codegen/stub_options.h>
#include <grpcpp/impl/codegen/sync_stream.h>

class Position final {
 public:
  static constexpr char const* service_full_name() {
    return "Position";
  }
  class StubInterface {
   public:
    virtual ~StubInterface() {}
    virtual ::grpc::Status SendPosition(::grpc::ClientContext* context, const ::PositionRequest& request, ::PositionReply* response) = 0;
    std::unique_ptr< ::grpc::ClientAsyncResponseReaderInterface< ::PositionReply>> AsyncSendPosition(::grpc::ClientContext* context, const ::PositionRequest& request, ::grpc::CompletionQueue* cq) {
      return std::unique_ptr< ::grpc::ClientAsyncResponseReaderInterface< ::PositionReply>>(AsyncSendPositionRaw(context, request, cq));
    }
    std::unique_ptr< ::grpc::ClientAsyncResponseReaderInterface< ::PositionReply>> PrepareAsyncSendPosition(::grpc::ClientContext* context, const ::PositionRequest& request, ::grpc::CompletionQueue* cq) {
      return std::unique_ptr< ::grpc::ClientAsyncResponseReaderInterface< ::PositionReply>>(PrepareAsyncSendPositionRaw(context, request, cq));
    }
    class experimental_async_interface {
     public:
      virtual ~experimental_async_interface() {}
      virtual void SendPosition(::grpc::ClientContext* context, const ::PositionRequest* request, ::PositionReply* response, std::function<void(::grpc::Status)>) = 0;
      virtual void SendPosition(::grpc::ClientContext* context, const ::grpc::ByteBuffer* request, ::PositionReply* response, std::function<void(::grpc::Status)>) = 0;
      #ifdef GRPC_CALLBACK_API_NONEXPERIMENTAL
      virtual void SendPosition(::grpc::ClientContext* context, const ::PositionRequest* request, ::PositionReply* response, ::grpc::ClientUnaryReactor* reactor) = 0;
      #else
      virtual void SendPosition(::grpc::ClientContext* context, const ::PositionRequest* request, ::PositionReply* response, ::grpc::experimental::ClientUnaryReactor* reactor) = 0;
      #endif
      #ifdef GRPC_CALLBACK_API_NONEXPERIMENTAL
      virtual void SendPosition(::grpc::ClientContext* context, const ::grpc::ByteBuffer* request, ::PositionReply* response, ::grpc::ClientUnaryReactor* reactor) = 0;
      #else
      virtual void SendPosition(::grpc::ClientContext* context, const ::grpc::ByteBuffer* request, ::PositionReply* response, ::grpc::experimental::ClientUnaryReactor* reactor) = 0;
      #endif
    };
    #ifdef GRPC_CALLBACK_API_NONEXPERIMENTAL
    typedef class experimental_async_interface async_interface;
    #endif
    #ifdef GRPC_CALLBACK_API_NONEXPERIMENTAL
    async_interface* async() { return experimental_async(); }
    #endif
    virtual class experimental_async_interface* experimental_async() { return nullptr; }
  private:
    virtual ::grpc::ClientAsyncResponseReaderInterface< ::PositionReply>* AsyncSendPositionRaw(::grpc::ClientContext* context, const ::PositionRequest& request, ::grpc::CompletionQueue* cq) = 0;
    virtual ::grpc::ClientAsyncResponseReaderInterface< ::PositionReply>* PrepareAsyncSendPositionRaw(::grpc::ClientContext* context, const ::PositionRequest& request, ::grpc::CompletionQueue* cq) = 0;
  };
  class Stub final : public StubInterface {
   public:
    Stub(const std::shared_ptr< ::grpc::ChannelInterface>& channel);
    ::grpc::Status SendPosition(::grpc::ClientContext* context, const ::PositionRequest& request, ::PositionReply* response) override;
    std::unique_ptr< ::grpc::ClientAsyncResponseReader< ::PositionReply>> AsyncSendPosition(::grpc::ClientContext* context, const ::PositionRequest& request, ::grpc::CompletionQueue* cq) {
      return std::unique_ptr< ::grpc::ClientAsyncResponseReader< ::PositionReply>>(AsyncSendPositionRaw(context, request, cq));
    }
    std::unique_ptr< ::grpc::ClientAsyncResponseReader< ::PositionReply>> PrepareAsyncSendPosition(::grpc::ClientContext* context, const ::PositionRequest& request, ::grpc::CompletionQueue* cq) {
      return std::unique_ptr< ::grpc::ClientAsyncResponseReader< ::PositionReply>>(PrepareAsyncSendPositionRaw(context, request, cq));
    }
    class experimental_async final :
      public StubInterface::experimental_async_interface {
     public:
      void SendPosition(::grpc::ClientContext* context, const ::PositionRequest* request, ::PositionReply* response, std::function<void(::grpc::Status)>) override;
      void SendPosition(::grpc::ClientContext* context, const ::grpc::ByteBuffer* request, ::PositionReply* response, std::function<void(::grpc::Status)>) override;
      #ifdef GRPC_CALLBACK_API_NONEXPERIMENTAL
      void SendPosition(::grpc::ClientContext* context, const ::PositionRequest* request, ::PositionReply* response, ::grpc::ClientUnaryReactor* reactor) override;
      #else
      void SendPosition(::grpc::ClientContext* context, const ::PositionRequest* request, ::PositionReply* response, ::grpc::experimental::ClientUnaryReactor* reactor) override;
      #endif
      #ifdef GRPC_CALLBACK_API_NONEXPERIMENTAL
      void SendPosition(::grpc::ClientContext* context, const ::grpc::ByteBuffer* request, ::PositionReply* response, ::grpc::ClientUnaryReactor* reactor) override;
      #else
      void SendPosition(::grpc::ClientContext* context, const ::grpc::ByteBuffer* request, ::PositionReply* response, ::grpc::experimental::ClientUnaryReactor* reactor) override;
      #endif
     private:
      friend class Stub;
      explicit experimental_async(Stub* stub): stub_(stub) { }
      Stub* stub() { return stub_; }
      Stub* stub_;
    };
    class experimental_async_interface* experimental_async() override { return &async_stub_; }

   private:
    std::shared_ptr< ::grpc::ChannelInterface> channel_;
    class experimental_async async_stub_{this};
    ::grpc::ClientAsyncResponseReader< ::PositionReply>* AsyncSendPositionRaw(::grpc::ClientContext* context, const ::PositionRequest& request, ::grpc::CompletionQueue* cq) override;
    ::grpc::ClientAsyncResponseReader< ::PositionReply>* PrepareAsyncSendPositionRaw(::grpc::ClientContext* context, const ::PositionRequest& request, ::grpc::CompletionQueue* cq) override;
    const ::grpc::internal::RpcMethod rpcmethod_SendPosition_;
  };
  static std::unique_ptr<Stub> NewStub(const std::shared_ptr< ::grpc::ChannelInterface>& channel, const ::grpc::StubOptions& options = ::grpc::StubOptions());

  class Service : public ::grpc::Service {
   public:
    Service();
    virtual ~Service();
    virtual ::grpc::Status SendPosition(::grpc::ServerContext* context, const ::PositionRequest* request, ::PositionReply* response);
  };
  template <class BaseClass>
  class WithAsyncMethod_SendPosition : public BaseClass {
   private:
    void BaseClassMustBeDerivedFromService(const Service* /*service*/) {}
   public:
    WithAsyncMethod_SendPosition() {
      ::grpc::Service::MarkMethodAsync(0);
    }
    ~WithAsyncMethod_SendPosition() override {
      BaseClassMustBeDerivedFromService(this);
    }
    // disable synchronous version of this method
    ::grpc::Status SendPosition(::grpc::ServerContext* /*context*/, const ::PositionRequest* /*request*/, ::PositionReply* /*response*/) override {
      abort();
      return ::grpc::Status(::grpc::StatusCode::UNIMPLEMENTED, "");
    }
    void RequestSendPosition(::grpc::ServerContext* context, ::PositionRequest* request, ::grpc::ServerAsyncResponseWriter< ::PositionReply>* response, ::grpc::CompletionQueue* new_call_cq, ::grpc::ServerCompletionQueue* notification_cq, void *tag) {
      ::grpc::Service::RequestAsyncUnary(0, context, request, response, new_call_cq, notification_cq, tag);
    }
  };
  typedef WithAsyncMethod_SendPosition<Service > AsyncService;
  template <class BaseClass>
  class ExperimentalWithCallbackMethod_SendPosition : public BaseClass {
   private:
    void BaseClassMustBeDerivedFromService(const Service* /*service*/) {}
   public:
    ExperimentalWithCallbackMethod_SendPosition() {
    #ifdef GRPC_CALLBACK_API_NONEXPERIMENTAL
      ::grpc::Service::
    #else
      ::grpc::Service::experimental().
    #endif
        MarkMethodCallback(0,
          new ::grpc_impl::internal::CallbackUnaryHandler< ::PositionRequest, ::PositionReply>(
            [this](
    #ifdef GRPC_CALLBACK_API_NONEXPERIMENTAL
                   ::grpc::CallbackServerContext*
    #else
                   ::grpc::experimental::CallbackServerContext*
    #endif
                     context, const ::PositionRequest* request, ::PositionReply* response) { return this->SendPosition(context, request, response); }));}
    void SetMessageAllocatorFor_SendPosition(
        ::grpc::experimental::MessageAllocator< ::PositionRequest, ::PositionReply>* allocator) {
    #ifdef GRPC_CALLBACK_API_NONEXPERIMENTAL
      ::grpc::internal::MethodHandler* const handler = ::grpc::Service::GetHandler(0);
    #else
      ::grpc::internal::MethodHandler* const handler = ::grpc::Service::experimental().GetHandler(0);
    #endif
      static_cast<::grpc_impl::internal::CallbackUnaryHandler< ::PositionRequest, ::PositionReply>*>(handler)
              ->SetMessageAllocator(allocator);
    }
    ~ExperimentalWithCallbackMethod_SendPosition() override {
      BaseClassMustBeDerivedFromService(this);
    }
    // disable synchronous version of this method
    ::grpc::Status SendPosition(::grpc::ServerContext* /*context*/, const ::PositionRequest* /*request*/, ::PositionReply* /*response*/) override {
      abort();
      return ::grpc::Status(::grpc::StatusCode::UNIMPLEMENTED, "");
    }
    #ifdef GRPC_CALLBACK_API_NONEXPERIMENTAL
    virtual ::grpc::ServerUnaryReactor* SendPosition(
      ::grpc::CallbackServerContext* /*context*/, const ::PositionRequest* /*request*/, ::PositionReply* /*response*/)
    #else
    virtual ::grpc::experimental::ServerUnaryReactor* SendPosition(
      ::grpc::experimental::CallbackServerContext* /*context*/, const ::PositionRequest* /*request*/, ::PositionReply* /*response*/)
    #endif
      { return nullptr; }
  };
  #ifdef GRPC_CALLBACK_API_NONEXPERIMENTAL
  typedef ExperimentalWithCallbackMethod_SendPosition<Service > CallbackService;
  #endif

  typedef ExperimentalWithCallbackMethod_SendPosition<Service > ExperimentalCallbackService;
  template <class BaseClass>
  class WithGenericMethod_SendPosition : public BaseClass {
   private:
    void BaseClassMustBeDerivedFromService(const Service* /*service*/) {}
   public:
    WithGenericMethod_SendPosition() {
      ::grpc::Service::MarkMethodGeneric(0);
    }
    ~WithGenericMethod_SendPosition() override {
      BaseClassMustBeDerivedFromService(this);
    }
    // disable synchronous version of this method
    ::grpc::Status SendPosition(::grpc::ServerContext* /*context*/, const ::PositionRequest* /*request*/, ::PositionReply* /*response*/) override {
      abort();
      return ::grpc::Status(::grpc::StatusCode::UNIMPLEMENTED, "");
    }
  };
  template <class BaseClass>
  class WithRawMethod_SendPosition : public BaseClass {
   private:
    void BaseClassMustBeDerivedFromService(const Service* /*service*/) {}
   public:
    WithRawMethod_SendPosition() {
      ::grpc::Service::MarkMethodRaw(0);
    }
    ~WithRawMethod_SendPosition() override {
      BaseClassMustBeDerivedFromService(this);
    }
    // disable synchronous version of this method
    ::grpc::Status SendPosition(::grpc::ServerContext* /*context*/, const ::PositionRequest* /*request*/, ::PositionReply* /*response*/) override {
      abort();
      return ::grpc::Status(::grpc::StatusCode::UNIMPLEMENTED, "");
    }
    void RequestSendPosition(::grpc::ServerContext* context, ::grpc::ByteBuffer* request, ::grpc::ServerAsyncResponseWriter< ::grpc::ByteBuffer>* response, ::grpc::CompletionQueue* new_call_cq, ::grpc::ServerCompletionQueue* notification_cq, void *tag) {
      ::grpc::Service::RequestAsyncUnary(0, context, request, response, new_call_cq, notification_cq, tag);
    }
  };
  template <class BaseClass>
  class ExperimentalWithRawCallbackMethod_SendPosition : public BaseClass {
   private:
    void BaseClassMustBeDerivedFromService(const Service* /*service*/) {}
   public:
    ExperimentalWithRawCallbackMethod_SendPosition() {
    #ifdef GRPC_CALLBACK_API_NONEXPERIMENTAL
      ::grpc::Service::
    #else
      ::grpc::Service::experimental().
    #endif
        MarkMethodRawCallback(0,
          new ::grpc_impl::internal::CallbackUnaryHandler< ::grpc::ByteBuffer, ::grpc::ByteBuffer>(
            [this](
    #ifdef GRPC_CALLBACK_API_NONEXPERIMENTAL
                   ::grpc::CallbackServerContext*
    #else
                   ::grpc::experimental::CallbackServerContext*
    #endif
                     context, const ::grpc::ByteBuffer* request, ::grpc::ByteBuffer* response) { return this->SendPosition(context, request, response); }));
    }
    ~ExperimentalWithRawCallbackMethod_SendPosition() override {
      BaseClassMustBeDerivedFromService(this);
    }
    // disable synchronous version of this method
    ::grpc::Status SendPosition(::grpc::ServerContext* /*context*/, const ::PositionRequest* /*request*/, ::PositionReply* /*response*/) override {
      abort();
      return ::grpc::Status(::grpc::StatusCode::UNIMPLEMENTED, "");
    }
    #ifdef GRPC_CALLBACK_API_NONEXPERIMENTAL
    virtual ::grpc::ServerUnaryReactor* SendPosition(
      ::grpc::CallbackServerContext* /*context*/, const ::grpc::ByteBuffer* /*request*/, ::grpc::ByteBuffer* /*response*/)
    #else
    virtual ::grpc::experimental::ServerUnaryReactor* SendPosition(
      ::grpc::experimental::CallbackServerContext* /*context*/, const ::grpc::ByteBuffer* /*request*/, ::grpc::ByteBuffer* /*response*/)
    #endif
      { return nullptr; }
  };
  template <class BaseClass>
  class WithStreamedUnaryMethod_SendPosition : public BaseClass {
   private:
    void BaseClassMustBeDerivedFromService(const Service* /*service*/) {}
   public:
    WithStreamedUnaryMethod_SendPosition() {
      ::grpc::Service::MarkMethodStreamed(0,
        new ::grpc::internal::StreamedUnaryHandler< ::PositionRequest, ::PositionReply>(std::bind(&WithStreamedUnaryMethod_SendPosition<BaseClass>::StreamedSendPosition, this, std::placeholders::_1, std::placeholders::_2)));
    }
    ~WithStreamedUnaryMethod_SendPosition() override {
      BaseClassMustBeDerivedFromService(this);
    }
    // disable regular version of this method
    ::grpc::Status SendPosition(::grpc::ServerContext* /*context*/, const ::PositionRequest* /*request*/, ::PositionReply* /*response*/) override {
      abort();
      return ::grpc::Status(::grpc::StatusCode::UNIMPLEMENTED, "");
    }
    // replace default version of method with streamed unary
    virtual ::grpc::Status StreamedSendPosition(::grpc::ServerContext* context, ::grpc::ServerUnaryStreamer< ::PositionRequest,::PositionReply>* server_unary_streamer) = 0;
  };
  typedef WithStreamedUnaryMethod_SendPosition<Service > StreamedUnaryService;
  typedef Service SplitStreamedService;
  typedef WithStreamedUnaryMethod_SendPosition<Service > StreamedService;
};


#endif  // GRPC_road_2eproto__INCLUDED
