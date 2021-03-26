package com.latihanlks.pc_kab_klaten_joko_supriyanto;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.util.Log;
import android.util.Xml;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import org.json.JSONException;
import org.json.JSONObject;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.OutputStreamWriter;
import java.net.HttpURLConnection;
import java.net.URI;
import java.net.URISyntaxException;
import java.net.URL;
import java.net.URLConnection;
import java.nio.charset.Charset;
import java.util.concurrent.CompletableFuture;

public class LoginActivity extends AppCompatActivity {

    public String token ;
    private static final String TAG = "LoginActivity";

    EditText email;
    EditText password;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);

         email = findViewById(R.id.editTextTextEmailAddress);
         password = findViewById(R.id.editTextTextPassword);

        ((Button)findViewById(R.id.btnLogin)).setOnClickListener((View.OnClickListener) v -> {

            if(email.getText().toString().matches("") || password.getText().toString().matches("")){
                Toast.makeText(this, "Email dan password tidak boleh kosong.", Toast.LENGTH_SHORT).show();
                return;
            }else{
                CompletableFuture.supplyAsync(() -> {
                    StringBuilder result = new StringBuilder();
                    try {
                        HttpURLConnection connection = (HttpURLConnection)new URL("http://10.0.2.2:5000/api/Auth").openConnection();
                        connection.setRequestMethod("POST");
                        connection.setRequestProperty("Accept", "application/json");
                        connection.setRequestProperty("Content-type", "application/json");
                        connection.setDoOutput(true);

                        JSONObject object = new JSONObject();
                        object.put("email", email.getText().toString());
                        object.put("password", password.getText().toString());

                        try(OutputStreamWriter outputStreamWriter = new OutputStreamWriter(connection.getOutputStream())){
                            outputStreamWriter.write(object.toString());
                            Log.i(TAG, "onCreate: " + outputStreamWriter.toString());
                        }

                        Log.i(TAG, "onCreate: " + connection.getResponseMessage());

                        Log.i(TAG, "onCreate: " + connection.getResponseCode());

                        try(BufferedReader bufferedReader = new BufferedReader(new InputStreamReader(connection.getInputStream()))){
                            result.append(bufferedReader.readLine());
                        }

                        return result;
                    } catch (IOException | JSONException e) {
                        e.printStackTrace();
                        return result;
                    }
                }).thenAccept((result) -> {
                    runOnUiThread(() -> {
                        if(result.toString().trim().equals("")){
                            Toast.makeText(this, "Email atau password salah, tolong masukan lagi.", Toast.LENGTH_SHORT).show();
                        }else{
                            token  = result.toString();
                            Toast.makeText(this, "Login sukses", Toast.LENGTH_SHORT).show();
                            Log.i(TAG, "onCreate: Login sukses" + result.toString() );
                            email.setText("");
                            password.setText("");

                            Intent intent = new Intent(this, MainActivity.class   );
                            intent.putExtra("token", token);
                            startActivity(intent);
                        }
                    });
                });
            }
        });
    }
}