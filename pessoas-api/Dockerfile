FROM openjdk:8-jdk-alpine
ARG JAR_FILE=target/*.jar
COPY ${JAR_FILE} /var/www/app.jar
EXPOSE 5555
ENTRYPOINT ["java","-jar","/var/www/app.jar"]