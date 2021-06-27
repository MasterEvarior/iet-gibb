package dev.giannin.securitymodulebackend.service;

import dev.giannin.securitymodulebackend.model.command.CommandException;
import dev.giannin.securitymodulebackend.model.command.CommandResultDto;
import org.springframework.stereotype.Service;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.util.Arrays;

@Service
public class CommandService {

    private final String[] allowedOptions = new String[]{"s", "d", "h"};

    public CommandResultDto executeCommand(String[] options) throws CommandException {
        final StringBuilder resultBuilder = new StringBuilder();

        if (!areArgumentsValid(options)) {
            throw new IllegalArgumentException("One of your supplied parameters is not valid");
        }

        final String buildOptions = buildOptions(options);
        try {
            Runtime r = Runtime.getRuntime();
            Process p;
            if (options == null) {
                System.out.println("Executing command 'ip a'");
                p = r.exec("ip a");
            } else {
                System.out.println("Executing command 'ip " + buildOptions + " a'");
                p = r.exec("ip " + buildOptions + " a");
            }
            p.waitFor();
            BufferedReader b = new BufferedReader(new InputStreamReader(p.getInputStream()));
            String line;

            while ((line = b.readLine()) != null) {
                resultBuilder.append(line);
            }

            b.close();

        } catch (IOException | InterruptedException e) {
            throw new CommandException(e.getMessage());
        }

        return new CommandResultDto(resultBuilder.toString());
    }

    private String buildOptions(String[] options) {
        if (options == null) {
            return "";
        }

        StringBuilder builder = new StringBuilder();
        for (String option : options) {
            builder.append(" -").append(option);
        }

        return builder.toString();
    }

    private boolean areArgumentsValid(String[] options) {
        if (options == null) {
            return true;
        }

        for (String option : options) {
            boolean result = Arrays.asList(this.allowedOptions).contains(option);
            if (!result) {
                return false;
            }
        }
        return true;
    }
}
