# Bắt đầu từ image .NET 7 SDK
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

# Thiết lập thư mục làm việc
WORKDIR /app

# Sao chép tất cả file trong thư mục gốc vào thư mục làm việc
COPY . ./

# Restore dependencies
RUN dotnet restore

# Build dự án
RUN dotnet publish -c Release -o /app/publish

# Bắt đầu từ image .NET 7 runtime để chạy ứng dụng
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base

WORKDIR /app
EXPOSE 80

# Copy output từ build image vào runtime image
COPY --from=build /app/publish .

# Lệnh chạy ứng dụng
ENTRYPOINT ["dotnet", "DoAn.dll"]
